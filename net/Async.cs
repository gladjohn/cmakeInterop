using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class Async : IDisposable
    {
        MSALRUNTIME_ASYNC_HANDLE _hAsync;
        internal Async(MSALRUNTIME_ASYNC_HANDLE hAsync)
        {
            _hAsync = hAsync;
        }

        public void Cancel()
        {
            if (_hAsync != null)
            {
                API.CPU.CancelAsyncOperation(_hAsync);
            }
        }

        public void Dispose()
        {
            if (_hAsync != null)
            {
                _hAsync.Dispose();
                _hAsync = null;
            }
        }
    }

    internal class MSALRUNTIME_ASYNC_HANDLE : Handle
    {
        public MSALRUNTIME_ASYNC_HANDLE()
        {
        }

        protected override void Release()
        {
            API.CPU.ReleaseAsyncHandle(this.handle);
        }
    }

    internal abstract partial class API
    {
        public abstract void ReleaseAsyncHandle(IntPtr asyncHandle);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAsyncHandle(IntPtr asyncHandle);

            public override void ReleaseAsyncHandle(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAsyncHandle(asyncHandle));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAsyncHandle(IntPtr asyncHandle);

            public override void ReleaseAsyncHandle(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAsyncHandle(asyncHandle));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAsyncHandle(IntPtr asyncHandle);

            public override void ReleaseAsyncHandle(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAsyncHandle(asyncHandle));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAsyncHandle(IntPtr asyncHandle);

            public override void ReleaseAsyncHandle(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAsyncHandle(asyncHandle));
            }
        }

        public abstract void CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override void CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_CancelAsyncOperation(asyncHandle));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override void CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_CancelAsyncOperation(asyncHandle));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override void CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_CancelAsyncOperation(asyncHandle));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override void CancelAsyncOperation(MSALRUNTIME_ASYNC_HANDLE asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_CancelAsyncOperation(asyncHandle));
            }
        }

    }

}
