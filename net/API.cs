using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.NativeInterop
{
    internal abstract partial class API
    {
        #region Resolve native DLL

        /// <summary>
        /// Help .NET locate the native dll. See details at: https://docs.microsoft.com/en-us/windows/win32/dlls/dynamic-link-library-search-order. 
        /// 
        /// The default order is: 
        /// 
        /// - same directory as the app
        /// - other directories like system and windows and the current dir
        /// - directories in the PATH
        /// 
        /// Out of all of these, the PATH is the simplest to modify.
        /// </summary>
        /// <remarks>
        /// Avoid using LoadLibrary as it is not compatible with UWP.
        /// </remarks>
        static API()
        {
            // .NET Fwk doesn't handle native DLLs very well, because it copies the 32 bit native DLL in the folder with the app, in AnyCpu configuration
            // But the process is mostly 64 bit. So we have to manually help the DllResolver find the 64 bit DLL
            if (Platform.IsRunningOnNetFramework())
            {
                // based on https://stackoverflow.com/questions/2864673/specify-the-search-path-for-dllimport-in-net
                string packageLocationInRuntime = GetPackagePath();
                var path = new[] { Environment.GetEnvironmentVariable("PATH") ?? string.Empty };
                string newPath = string.Join(Path.PathSeparator.ToString(), path.Concat(new[] { packageLocationInRuntime }));

                Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.Process);                
            }
        }


        public static readonly API CPU = CreateAPI();

        partial class x86 : API
        {
            public const string Name = @"msalruntime_x86";
        }

        partial class x64 : API
        {
            public const string Name = @"msalruntime";
        }

        partial class arm : API
        {
            public const string Name = @"msalruntime_arm";
        }

        partial class arm64 : API
        {
            public const string Name = @"msalruntime_arm64";
        }  
        
        private static string GetPackagePath()
        {
            string execDir = Platform.GetExecutingAssemblyDirectory();


            string runtimeNameInPackage;
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    runtimeNameInPackage = "win-x86";
                    break;
                case Architecture.X64:
                    runtimeNameInPackage = "win-x64";
                    break;
                case Architecture.Arm:
                    runtimeNameInPackage = "win-arm";
                    break;
                case Architecture.Arm64:
                    runtimeNameInPackage = "win-arm64";
                    break;
                default:
                    throw new PlatformNotSupportedException();
            }

            // e.g. runtimes\win-x86\native
            string runtimePath = System.IO.Path.Combine(execDir, "runtimes", runtimeNameInPackage, "native");

            return runtimePath;
        }

        private static API CreateAPI()
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X86:
                    return new x86();
                case Architecture.X64:
                    return new x64();
                case Architecture.Arm:
                    return new arm();
                case Architecture.Arm64:
                    return new arm64();
                default:
                    throw new PlatformNotSupportedException();
            }
        }

        #endregion

        #region Helpers

        private delegate void MSALRUNTIME_COMPLETION_ROUTINE(IntPtr hResponse, IntPtr callbackData);

        private static void CallbackCompletion(IntPtr hResponse, IntPtr callbackData)
        {
            CallbackCompletion<MSALRUNTIME_AUTH_RESULT_HANDLE>(hResponse, callbackData, (h) => new MSALRUNTIME_AUTH_RESULT_HANDLE(h));
        }

        private static void SignOutCallbackCompletion(IntPtr hResponse, IntPtr callbackData)
        {
            CallbackCompletion<MSALRUNTIME_SIGNOUT_RESULT_HANDLE>(hResponse, callbackData, (h) => new MSALRUNTIME_SIGNOUT_RESULT_HANDLE(h));
        }

        private static void CallbackCompletion<T>(IntPtr hResponse, IntPtr callbackData, Func<IntPtr, T> convert)
        {
            GCHandle gchCallback = GCHandle.FromIntPtr(callbackData);
            Action<T> callback = gchCallback.Target as Action<T>;
            gchCallback.Free();

            if (callback != null)
            {
                callback(convert(hResponse));
            }
        }

        private delegate MSALRUNTIME_ERROR_HANDLE MSALAsync(MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

     


        private void ThrowIfFailed(MSALRUNTIME_ERROR_HANDLE hError)
        {
            Error error = Error.TryCreate(hError);

            if (error != null)
            {
                throw new MsalRuntimeException(error);
            }
        }

        protected delegate MSALRUNTIME_ERROR_HANDLE APIFunc(char[] result, ref Int32 bufferSize);

        protected string GetString(APIFunc func)
        {
            char[] result = null;
            int size = 0;
            MSALRUNTIME_ERROR_HANDLE error = func(null, ref size);

            if (error.IsSuccess)
            {
                error.Dispose();
                return string.Empty;
            }

            if (GetStatus(error) == ResponseStatus.InsufficientBuffer && size > 0)
            {
                error.Dispose();
                result = new char[size];

                error = func(result, ref size);
            }
            ThrowIfFailed(error);

            if (result != null && size > 0)
                return new string(result, 0, size - 1);

            return string.Empty;
        }

        #endregion
    }
}
