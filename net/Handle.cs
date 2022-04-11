using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.NativeInterop
{
    internal abstract class Handle : SafeHandle
    {
        bool _releaseModule = true;
        public Handle(bool releaseModule = true) : base(IntPtr.Zero, true)
        {
            _releaseModule = releaseModule;

            if (_releaseModule)
            {
                Module.AddRef();
            }
        }

        public override bool IsInvalid => this.handle == IntPtr.Zero;

        protected override sealed bool ReleaseHandle()
        {
            try
            {
                Release();
            }
            catch (MsalRuntimeException)
            {
                return false;
            }
            return true;
        }

        protected abstract void Release();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_releaseModule)
            {
                Module.RemoveRef();
                _releaseModule = false;
            }
        }
    }

}
