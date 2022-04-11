using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class AuthResult : IDisposable
    {
        private MSALRUNTIME_AUTH_RESULT_HANDLE _hAuthResult;
        private Lazy<Account> _account;
        private Lazy<string> _accessToken;
        private Lazy<string> _idToken;
        private Lazy<string> _grantedScopes;
        private Lazy<DateTime> _expiresOn;
        private Lazy<Error> _error;
        private Lazy<string> _telemetryData;

        internal AuthResult(MSALRUNTIME_AUTH_RESULT_HANDLE hAuthResult)
        {
            _hAuthResult = hAuthResult;
            _account = new Lazy<Account>(() =>
            {

                MSALRUNTIME_ACCOUNT_HANDLE hAccount = API.CPU.GetAccount(_hAuthResult);

                if (hAccount.IsInvalid)
                {
                    hAccount.Dispose();
                    return null;
                }

                return new Account(hAccount);
            });

            _accessToken = new Lazy<string>(() => API.CPU.GetAccessToken(_hAuthResult));
            _idToken = new Lazy<string>(() => API.CPU.GetIdToken(_hAuthResult));
            _grantedScopes = new Lazy<string>(() => API.CPU.GetGrantedScopes(_hAuthResult));
            _expiresOn = new Lazy<DateTime>(() =>
            {
                Int64 expires = API.CPU.GetExpiresOn(_hAuthResult);
                return (new DateTime(1970, 1, 1)).AddSeconds(expires);
            });
            _error = new Lazy<Error>(() => API.CPU.GetError(_hAuthResult));
            _telemetryData = new Lazy<string>(() => API.CPU.GetTelemetryData(_hAuthResult));
        }

        public bool IsSuccess => Error == null;
        public Account Account => _account.Value;
        public string AccessToken => _accessToken.Value;
        public string IdToken => _idToken.Value;
        public string GrantedScopes => _grantedScopes.Value;
        public DateTime ExpiresOn => _expiresOn.Value;
        public Error Error => _error.Value;
        public string TelemetryData => _telemetryData.Value;

        public void Dispose()
        {
            if (_hAuthResult != null)
            {
                _hAuthResult.Dispose();
                _hAuthResult = null;
            }
        }
    }

    internal class MSALRUNTIME_AUTH_RESULT_HANDLE : Handle
    {
        public MSALRUNTIME_AUTH_RESULT_HANDLE()
        {
        }

        public MSALRUNTIME_AUTH_RESULT_HANDLE(IntPtr hndl)
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
        #region MSALRUNTIME_AUTH_RESULT_HANDLE

        private static Task<AuthResult> CreateAsync(MSALAsync asyncFunc, out Async asyncObj)
        {
            return CreateAsync<AuthResult, MSALRUNTIME_AUTH_RESULT_HANDLE>(asyncFunc, (h) => new AuthResult(h), CallbackCompletion, out asyncObj);
        }

        public abstract void ReleaseAuthResult(IntPtr authResult);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthResult(IntPtr authResult);

            public override void ReleaseAuthResult(IntPtr authResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthResult(authResult));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthResult(IntPtr authResult);

            public override void ReleaseAuthResult(IntPtr authResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthResult(authResult));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthResult(IntPtr authResult);

            public override void ReleaseAuthResult(IntPtr authResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthResult(authResult));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthResult(IntPtr authResult);

            public override void ReleaseAuthResult(IntPtr authResult)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthResult(authResult));
            }
        }

        public abstract MSALRUNTIME_ACCOUNT_HANDLE GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, out MSALRUNTIME_ACCOUNT_HANDLE account);

            public override MSALRUNTIME_ACCOUNT_HANDLE GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                MSALRUNTIME_ACCOUNT_HANDLE account = null;
                ThrowIfFailed(MSALRUNTIME_GetAccount(authResult, out account));
                return account;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, out MSALRUNTIME_ACCOUNT_HANDLE account);

            public override MSALRUNTIME_ACCOUNT_HANDLE GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                MSALRUNTIME_ACCOUNT_HANDLE account = null;
                ThrowIfFailed(MSALRUNTIME_GetAccount(authResult, out account));
                return account;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, out MSALRUNTIME_ACCOUNT_HANDLE account);

            public override MSALRUNTIME_ACCOUNT_HANDLE GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                MSALRUNTIME_ACCOUNT_HANDLE account = null;
                ThrowIfFailed(MSALRUNTIME_GetAccount(authResult, out account));
                return account;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, out MSALRUNTIME_ACCOUNT_HANDLE account);

            public override MSALRUNTIME_ACCOUNT_HANDLE GetAccount(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                MSALRUNTIME_ACCOUNT_HANDLE account = null;
                ThrowIfFailed(MSALRUNTIME_GetAccount(authResult, out account));
                return account;
            }
        }

        public abstract string GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetIdToken(authResult, value, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetIdToken(authResult, value, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetIdToken(authResult, value, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetIdToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetIdToken(authResult, value, ref bufferSize));
            }
        }

        public abstract string GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccessToken(authResult, value, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccessToken(authResult, value, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccessToken(authResult, value, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetAccessToken(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetAccessToken(authResult, value, ref bufferSize));
            }
        }

        public abstract string GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetGrantedScopes(authResult, value, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetGrantedScopes(authResult, value, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetGrantedScopes(authResult, value, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult, char[] value, ref Int32 bufferSize);
            public override string GetGrantedScopes(MSALRUNTIME_AUTH_RESULT_HANDLE authResult)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetGrantedScopes(authResult, value, ref bufferSize));
            }
        }

        public abstract Int64 GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle);
        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out Int64 result);

            public override Int64 GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                Int64 result;
                ThrowIfFailed(MSALRUNTIME_GetExpiresOn(handle, out result));
                return result;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out Int64 result);

            public override Int64 GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                Int64 result;
                ThrowIfFailed(MSALRUNTIME_GetExpiresOn(handle, out result));
                return result;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out Int64 result);

            public override Int64 GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                Int64 result;
                ThrowIfFailed(MSALRUNTIME_GetExpiresOn(handle, out result));
                return result;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out Int64 result);

            public override Int64 GetExpiresOn(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                Int64 result;
                ThrowIfFailed(MSALRUNTIME_GetExpiresOn(handle, out result));
                return result;
            }
        }

        public Error GetError(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
        {
            return Error.TryCreate(API.CPU.GetErrorInternal(handle));
        }

        protected abstract MSALRUNTIME_ERROR_HANDLE GetErrorInternal(MSALRUNTIME_AUTH_RESULT_HANDLE handle);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetError(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetErrorInternal(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetError(handle, out result));
                return result;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetError(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetErrorInternal(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetError(handle, out result));
                return result;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetError(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetErrorInternal(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetError(handle, out result));
                return result;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetError(MSALRUNTIME_AUTH_RESULT_HANDLE handle, out MSALRUNTIME_ERROR_HANDLE result);

            protected override MSALRUNTIME_ERROR_HANDLE GetErrorInternal(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                MSALRUNTIME_ERROR_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_GetError(handle, out result));
                return result;
            }
        }

        public abstract string GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetTelemetryData(handle, value, ref bufferSize));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetTelemetryData(handle, value, ref bufferSize));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetTelemetryData(handle, value, ref bufferSize));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle, char[] value, ref Int32 bufferSize);
            public override string GetTelemetryData(MSALRUNTIME_AUTH_RESULT_HANDLE handle)
            {
                return GetString((char[] value, ref Int32 bufferSize) => MSALRUNTIME_GetTelemetryData(handle, value, ref bufferSize));
            }
        }

        #endregion
    }
}
