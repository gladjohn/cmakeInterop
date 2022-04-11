using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class SignOutResult : IDisposable
    {
        private MSALRUNTIME_SIGNOUT_RESULT_HANDLE _hSignOutResult;
        private Lazy<Error> _error;
        private Lazy<string> _telemetryData;

        internal SignOutResult(MSALRUNTIME_SIGNOUT_RESULT_HANDLE hSignOutResult)
        {
            _hSignOutResult = hSignOutResult;
            _error = new Lazy<Error>(() => API.CPU.GetSignOutError(_hSignOutResult));
            _telemetryData = new Lazy<string>(() => API.CPU.GetSignOutTelemetryData(_hSignOutResult));
        }

        public bool IsSuccess => Error == null;
        public Error Error => _error.Value;
        public string TelemetryData => _telemetryData.Value;

        public void Dispose()
        {
            if (_hSignOutResult != null)
            {
                _hSignOutResult.Dispose();
                _hSignOutResult = null;
            }
        }
    }

    internal class MSALRUNTIME_SIGNOUT_RESULT_HANDLE : Handle
    {
        public MSALRUNTIME_SIGNOUT_RESULT_HANDLE()
        {
        }

        public MSALRUNTIME_SIGNOUT_RESULT_HANDLE(IntPtr hndl)
        {
            this.SetHandle(hndl);
        }

        protected override void Release()
        {
            API.CPU.ReleaseAuthResult(this.handle);
        }
    }

    internal abstract partial class API
    {

        public abstract void ReleaseSignOutResult(IntPtr signOutResult);

        private static Task<SignOutResult> CreateSignOutAsync(MSALAsync asyncFunc, out Async asyncObj)
        {
            return CreateAsync<SignOutResult, MSALRUNTIME_SIGNOUT_RESULT_HANDLE>(asyncFunc, (h) => new SignOutResult(h), SignOutCallbackCompletion, out asyncObj);
        }

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseSignOutResult(IntPtr signOutResult);

            public override void ReleaseSignOutResult(IntPtr signOutResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseSignOutResult(signOutResult));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseSignOutResult(IntPtr signOutResult);

            public override void ReleaseSignOutResult(IntPtr signOutResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseSignOutResult(signOutResult));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseSignOutResult(IntPtr signOutResult);

            public override void ReleaseSignOutResult(IntPtr signOutResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseSignOutResult(signOutResult));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseSignOutResult(IntPtr signOutResult);

            public override void ReleaseSignOutResult(IntPtr signOutResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseSignOutResult(signOutResult));
            }
        }

        public Error GetSignOutError(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
        {
            return Error.TryCreate(API.CPU.GetSignOutErrorInternal(handle));
        }

        protected abstract MSALRUNTIME_ERROR_HANDLE GetSignOutErrorInternal(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutError(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetSignOutErrorInternal(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetSignOutError(handle, out result));
                return result;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutError(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetSignOutErrorInternal(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetSignOutError(handle, out result));
                return result;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutError(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetSignOutErrorInternal(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetSignOutError(handle, out result));
                return result;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutError(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetSignOutErrorInternal(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetSignOutError(handle, out result));
                return result;
            }
        }

        public abstract string GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetSignOutTelemetryData(handle, value, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetSignOutTelemetryData(handle, value, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetSignOutTelemetryData(handle, value, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetSignOutTelemetryData(MSALRUNTIME_SIGNOUT_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetSignOutTelemetryData(handle, value, ref bufferSize));
            }
        }
    }
}
