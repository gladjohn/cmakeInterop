using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Identity.Client.NativeInterop
{
    /// <summary>
    /// An internal class used to track native handles
    /// </summary>
    internal class Module
    {
        private static int s_handleCount = 0;
        private static object s_lockRuntime = new object();

        public static int HandleCount
        {
            get
            {
                return s_handleCount;
            }
        }

        internal static void AddRef()
        {
            lock (s_lockRuntime)
            {
                s_handleCount++;
                if (s_handleCount == 1)
                {
                    API.CPU.Startup();
                }
            }
        }

        internal static void RemoveRef()
        {
            lock (s_lockRuntime)
            {
                s_handleCount--;
                if (s_handleCount == 0)
                {
                    API.CPU.Shutdown();
                }
            }
        }

        public static int RefCount()
        {
            return s_handleCount;
        }
    }

    internal class MSALRUNTIME_ERROR_HANDLE_MODULE : MSALRUNTIME_ERROR_HANDLE
    {
        public MSALRUNTIME_ERROR_HANDLE_MODULE() : base(releaseModule: false)
        {
        }
    }

    internal abstract partial class API
    {
        public abstract void Startup();

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE_MODULE MSALRUNTIME_Startup();

            public override void Startup()
            {
                ThrowIfFailed(MSALRUNTIME_Startup());
            }

        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE_MODULE MSALRUNTIME_Startup();

            public override void Startup()
            {
                ThrowIfFailed(MSALRUNTIME_Startup());
            }

        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE_MODULE MSALRUNTIME_Startup();

            public override void Startup()
            {
                ThrowIfFailed(MSALRUNTIME_Startup());
            }

        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE_MODULE MSALRUNTIME_Startup();

            public override void Startup()
            {
                ThrowIfFailed(MSALRUNTIME_Startup());
            }
        }


        public abstract void Shutdown();

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern void MSALRUNTIME_Shutdown();

            public override void Shutdown()
            {
                MSALRUNTIME_Startup();
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern void MSALRUNTIME_Shutdown();

            public override void Shutdown()
            {
                MSALRUNTIME_Shutdown();
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern void MSALRUNTIME_Shutdown();

            public override void Shutdown()
            {
                MSALRUNTIME_Shutdown();
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern void MSALRUNTIME_Shutdown();

            public override void Shutdown()
            {
                MSALRUNTIME_Shutdown();
            }
        }
    }

}
