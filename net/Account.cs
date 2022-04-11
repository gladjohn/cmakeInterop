using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class Account : IDisposable
    {
        private MSALRUNTIME_ACCOUNT_HANDLE _hAccount;
        private Lazy<string> _id;
        private Lazy<string> _client_info;
        private PropertyROCollection _properties;

        internal Account(MSALRUNTIME_ACCOUNT_HANDLE hAccount)
        {
            _hAccount = hAccount;
            _id = new Lazy<string>(() => API.CPU.GetAccountId(_hAccount));
            _client_info = new Lazy<string>(() => API.CPU.GetClientInfo(_hAccount));
            _properties = new PropertyROCollection((string key) => API.CPU.GetAccountProperty(_hAccount, key));
        }

        public string Id => _id.Value;
        public string ClientInfo => _client_info.Value;
        public PropertyROCollection Properties => _properties;
        internal MSALRUNTIME_ACCOUNT_HANDLE Handle => _hAccount;

        public void Dispose()
        {
            if (_hAccount != null)
            {
                _hAccount.Dispose();
                _hAccount = null;
            }
        }
    }

    internal class MSALRUNTIME_ACCOUNT_HANDLE : Handle
    {
        public MSALRUNTIME_ACCOUNT_HANDLE()
        {
        }

        protected override void Release()
        {
            API.CPU.ReleaseAccount(this.handle);
        }
    }

    internal abstract partial class API
    {
        public abstract void ReleaseAccount(IntPtr account);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAccount(IntPtr account);

            public override void ReleaseAccount(IntPtr account)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAccount(account));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAccount(IntPtr account);

            public override void ReleaseAccount(IntPtr account)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAccount(account));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAccount(IntPtr account);

            public override void ReleaseAccount(IntPtr account)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAccount(account));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAccount(IntPtr account);

            public override void ReleaseAccount(IntPtr account)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAccount(account));
            }
        }

        public abstract string GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account, char[] accountId, ref Int32 bufferSize);
            public override string GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] accountId, ref Int32 bufferSize) => MSALRUNTIME_GetAccountId(account, accountId, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account, char[] accountId, ref Int32 bufferSize);
            public override string GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] accountId, ref Int32 bufferSize) => MSALRUNTIME_GetAccountId(account, accountId, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account, char[] accountId, ref Int32 bufferSize);
            public override string GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] accountId, ref Int32 bufferSize) => MSALRUNTIME_GetAccountId(account, accountId, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account, char[] accountId, ref Int32 bufferSize);
            public override string GetAccountId(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] accountId, ref Int32 bufferSize) => MSALRUNTIME_GetAccountId(account, accountId, ref bufferSize));
            }
        }

        public abstract string GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account, char[] value, ref Int32 bufferSize);
            public override string GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetClientInfo(account, value, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account, char[] value, ref Int32 bufferSize);
            public override string GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetClientInfo(account, value, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account, char[] value, ref Int32 bufferSize);
            public override string GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetClientInfo(account, value, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account, char[] value, ref Int32 bufferSize);
            public override string GetClientInfo(MSALRUNTIME_ACCOUNT_HANDLE account)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetClientInfo(account, value, ref bufferSize));
            }
        }

        public abstract string GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key, char[] value, ref Int32 bufferSize);
            public override string GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccountProperty(account, key, value, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key, char[] value, ref Int32 bufferSize);
            public override string GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccountProperty(account, key, value, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key, char[] value, ref Int32 bufferSize);
            public override string GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccountProperty(account, key, value, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key, char[] value, ref Int32 bufferSize);
            public override string GetAccountProperty(MSALRUNTIME_ACCOUNT_HANDLE account, string key)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccountProperty(account, key, value, ref bufferSize));
            }
        }
    }

}
