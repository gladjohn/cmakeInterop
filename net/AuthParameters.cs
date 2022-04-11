using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.NativeInterop
{
    public class AuthParameters : IDisposable
    {
        private MSALRUNTIME_AUTH_PARAMETERS_HANDLE _params;
        private string _requestedScopes;
        private string _redirectUri;
        private string _decodedClaims;
        private string _accessTokenToRenew;

        private PropertyCollection _properties;

        public AuthParameters(string clientId, string authority)
        {
            _params = API.CPU.CreateAuthParameters(clientId, authority);
            _properties = new PropertyCollection((key, value) => API.CPU.SetAdditionalParameter(_params, key, value));
        }

        public string RequestedScopes
        {
            get
            {
                return _requestedScopes;
            }

            set
            {
                API.CPU.SetRequestedScopes(_params, value);
                _requestedScopes = value;
            }
        }

        public string RedirectUri
        {
            get
            {
                return _redirectUri;
            }

            set
            {
                API.CPU.SetRedirectUri(_params, value);
                _redirectUri = value;
            }
        }

        public string DecodedClaims
        {
            get
            {
                return _decodedClaims;
            }

            set
            {
                API.CPU.SetDecodedClaims(_params, value);
                _decodedClaims = value;
            }
        }

        public string AccessTokenToRenew
        {
            get
            {
                return _accessTokenToRenew;
            }
            set
            {
                API.CPU.SetAccessTokenToRenew(_params, value);
                _accessTokenToRenew = value;
            }
        }

        public PropertyCollection Properties => _properties;

        internal MSALRUNTIME_AUTH_PARAMETERS_HANDLE Handle => _params;
        public void Dispose()
        {
            if (_params != null)
            {
                _params.Dispose();
                _params = null;
            }
        }
    }

    internal class MSALRUNTIME_AUTH_PARAMETERS_HANDLE : Handle
    {
        public MSALRUNTIME_AUTH_PARAMETERS_HANDLE()
        {
        }

        protected override void Release()
        {
            API.CPU.ReleaseAuthParameters(this.handle);
        }
    }

    internal abstract partial class API
    {
        #region MSALRUNTIME_AUTH_PARAMETERS_HANDLE

        public abstract void ReleaseAuthParameters(IntPtr asyncHandle);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthParameters(IntPtr asyncHandle);

            public override void ReleaseAuthParameters(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthParameters(asyncHandle));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthParameters(IntPtr asyncHandle);

            public override void ReleaseAuthParameters(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthParameters(asyncHandle));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthParameters(IntPtr asyncHandle);

            public override void ReleaseAuthParameters(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthParameters(asyncHandle));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReleaseAuthParameters(IntPtr asyncHandle);

            public override void ReleaseAuthParameters(IntPtr asyncHandle)
            {
                ThrowIfFailed(MSALRUNTIME_ReleaseAuthParameters(asyncHandle));
            }
        }

        public abstract MSALRUNTIME_AUTH_PARAMETERS_HANDLE CreateAuthParameters(string clientId, string authority);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CreateAuthParameters(string clientId, string authority, out MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters);
            public override MSALRUNTIME_AUTH_PARAMETERS_HANDLE CreateAuthParameters(string clientId, string authority)
            {
                MSALRUNTIME_AUTH_PARAMETERS_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_CreateAuthParameters(clientId, authority, out result));
                return result;
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CreateAuthParameters(string clientId, string authority, out MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters);
            public override MSALRUNTIME_AUTH_PARAMETERS_HANDLE CreateAuthParameters(string clientId, string authority)
            {
                MSALRUNTIME_AUTH_PARAMETERS_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_CreateAuthParameters(clientId, authority, out result));
                return result;
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CreateAuthParameters(string clientId, string authority, out MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters);
            public override MSALRUNTIME_AUTH_PARAMETERS_HANDLE CreateAuthParameters(string clientId, string authority)
            {
                MSALRUNTIME_AUTH_PARAMETERS_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_CreateAuthParameters(clientId, authority, out result));
                return result;
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_CreateAuthParameters(string clientId, string authority, out MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters);
            public override MSALRUNTIME_AUTH_PARAMETERS_HANDLE CreateAuthParameters(string clientId, string authority)
            {
                MSALRUNTIME_AUTH_PARAMETERS_HANDLE result;
                ThrowIfFailed(MSALRUNTIME_CreateAuthParameters(clientId, authority, out result));
                return result;
            }
        }

        public abstract void SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes);

            public override void SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes)
            {
                ThrowIfFailed(MSALRUNTIME_SetRequestedScopes(authParameters, scopes));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes);

            public override void SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes)
            {
                ThrowIfFailed(MSALRUNTIME_SetRequestedScopes(authParameters, scopes));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes);

            public override void SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes)
            {
                ThrowIfFailed(MSALRUNTIME_SetRequestedScopes(authParameters, scopes));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes);

            public override void SetRequestedScopes(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string scopes)
            {
                ThrowIfFailed(MSALRUNTIME_SetRequestedScopes(authParameters, scopes));
            }
        }

        public abstract void SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetDecodedClaims(handle, value));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetDecodedClaims(handle, value));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetDecodedClaims(handle, value));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetDecodedClaims(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetDecodedClaims(handle, value));
            }
        }

        public abstract void SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetRedirectUri(handle, value));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetRedirectUri(handle, value));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetRedirectUri(handle, value));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetRedirectUri(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetRedirectUri(handle, value));
            }
        }

        public abstract void SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAccessTokenToRenew(handle, value));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAccessTokenToRenew(handle, value));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAccessTokenToRenew(handle, value));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value);

            public override void SetAccessTokenToRenew(MSALRUNTIME_AUTH_PARAMETERS_HANDLE handle, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAccessTokenToRenew(handle, value));
            }
        }

        public abstract void SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value);

            public override void SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAdditionalParameter(authParameters, key, value));
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value);

            public override void SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAdditionalParameter(authParameters, key, value));
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value);

            public override void SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAdditionalParameter(authParameters, key, value));
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value);

            public override void SetAdditionalParameter(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string key, string value)
            {
                ThrowIfFailed(MSALRUNTIME_SetAdditionalParameter(authParameters, key, value));
            }
        }

        #endregion        

    }
}
