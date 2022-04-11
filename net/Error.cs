using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class Error
    {
        private ResponseStatus _status;
        private Int32 _errorCode;
        private Int32 _tag;
        private string _context;

        private Error(MSALRUNTIME_ERROR_HANDLE hError)
        {
            _status = API.CPU.GetStatus(hError);
            _errorCode = API.CPU.GetErrorCode(hError);
            _tag = API.CPU.GetTag(hError);
            _context = API.CPU.GetContext(hError);
            hError.Dispose();
        }

        internal static Error TryCreate(MSALRUNTIME_ERROR_HANDLE hError)
        {
            if (hError.IsSuccess)
            {
                // release fast, to free module.
                hError.Dispose();
                return null;
            }

            return new Error(hError);
        }

        public ResponseStatus Status => _status;
        public int ErrorCode => _errorCode;
        public int Tag => _tag;
        public string Context => _context;

        public override string ToString()
        {
            return $"Status: {Status}\r\n" +
                   (ErrorCode == 0 ? "" : $"Error: 0x{ErrorCode:x08}\r\n") +
                   $"Context: {Context}\r\n" +
                   $"Tag: 0x{Tag:x}";

        }
    }

    internal class MSALRUNTIME_ERROR_HANDLE : Handle
    {
        MSALRUNTIME_ERROR_HANDLE() : base(releaseModule: true)
        {
        }

        public MSALRUNTIME_ERROR_HANDLE(bool releaseModule) : base(releaseModule)
        {
        }

        protected override void Release()
        {
            API.CPU.ReleaseError(this.handle);
        }

        public bool IsSuccess => IsInvalid;

    }

    internal abstract partial class API
    {
        #region MSALRUNTIME_ERROR_HANDLE

        public abstract void ReleaseError(IntPtr error);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool MSALRUNTIME_ReleaseError(IntPtr error);

            public override void ReleaseError(IntPtr error)
            {
                MSALRUNTIME_ReleaseError(error);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool MSALRUNTIME_ReleaseError(IntPtr error);

            public override void ReleaseError(IntPtr error)
            {
                MSALRUNTIME_ReleaseError(error);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool MSALRUNTIME_ReleaseError(IntPtr error);

            public override void ReleaseError(IntPtr error)
            {
                MSALRUNTIME_ReleaseError(error);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool MSALRUNTIME_ReleaseError(IntPtr error);

            public override void ReleaseError(IntPtr error)
            {
                MSALRUNTIME_ReleaseError(error);
            }
        }

        public abstract ResponseStatus GetStatus(MSALRUNTIME_ERROR_HANDLE error);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetStatus(MSALRUNTIME_ERROR_HANDLE error, out ResponseStatus responseStatus);

            public override ResponseStatus GetStatus(MSALRUNTIME_ERROR_HANDLE error)
            {
                ResponseStatus result;
                ThrowIfFailed(MSALRUNTIME_GetStatus(error, out result));
                return result;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetStatus(MSALRUNTIME_ERROR_HANDLE error, out ResponseStatus responseStatus);

            public override ResponseStatus GetStatus(MSALRUNTIME_ERROR_HANDLE error)
            {
                ResponseStatus result;
                ThrowIfFailed(MSALRUNTIME_GetStatus(error, out result));
                return result;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetStatus(MSALRUNTIME_ERROR_HANDLE error, out ResponseStatus responseStatus);

            public override ResponseStatus GetStatus(MSALRUNTIME_ERROR_HANDLE error)
            {
                ResponseStatus result;
                ThrowIfFailed(MSALRUNTIME_GetStatus(error, out result));
                return result;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetStatus(MSALRUNTIME_ERROR_HANDLE error, out ResponseStatus responseStatus);

            public override ResponseStatus GetStatus(MSALRUNTIME_ERROR_HANDLE error)
            {
                ResponseStatus result;
                ThrowIfFailed(MSALRUNTIME_GetStatus(error, out result));
                return result;
            }
        }

        public abstract Int32 GetErrorCode(MSALRUNTIME_ERROR_HANDLE error);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetErrorCode(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetErrorCode(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetErrorCode(error, out result));
                return result;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetErrorCode(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetErrorCode(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetErrorCode(error, out result));
                return result;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetErrorCode(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetErrorCode(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetErrorCode(error, out result));
                return result;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetErrorCode(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetErrorCode(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetErrorCode(error, out result));
                return result;
            }
        }

        public abstract Int32 GetTag(MSALRUNTIME_ERROR_HANDLE error);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTag(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetTag(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetTag(error, out result));
                return result;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTag(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetTag(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetTag(error, out result));
                return result;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTag(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetTag(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetTag(error, out result));
                return result;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTag(MSALRUNTIME_ERROR_HANDLE error, out Int32 responseErrorCode);

            public override Int32 GetTag(MSALRUNTIME_ERROR_HANDLE error)
            {
                Int32 result;
                ThrowIfFailed(MSALRUNTIME_GetTag(error, out result));
                return result;
            }
        }

        public abstract string GetContext(MSALRUNTIME_ERROR_HANDLE error);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetContext(MSALRUNTIME_ERROR_HANDLE error, char[] context, ref Int32 bufferSize);

            public override string GetContext(MSALRUNTIME_ERROR_HANDLE error)
            {
                return GetString((char[] result, ref Int32 size) => MSALRUNTIME_GetContext(error, result, ref size));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetContext(MSALRUNTIME_ERROR_HANDLE error, char[] context, ref Int32 bufferSize);

            public override string GetContext(MSALRUNTIME_ERROR_HANDLE error)
            {
                return GetString((char[] result, ref Int32 size) => MSALRUNTIME_GetContext(error, result, ref size));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetContext(MSALRUNTIME_ERROR_HANDLE error, char[] context, ref Int32 bufferSize);

            public override string GetContext(MSALRUNTIME_ERROR_HANDLE error)
            {
                return GetString((char[] result, ref Int32 size) => MSALRUNTIME_GetContext(error, result, ref size));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetContext(MSALRUNTIME_ERROR_HANDLE error, char[] context, ref Int32 bufferSize);

            public override string GetContext(MSALRUNTIME_ERROR_HANDLE error)
            {
                return GetString((char[] result, ref Int32 size) => MSALRUNTIME_GetContext(error, result, ref size));
            }
        }

        #endregion


    }
}
