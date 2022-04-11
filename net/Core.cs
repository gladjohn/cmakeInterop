using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.NativeInterop
{
    /*
     * Bug 1: Invalid compilation of msalruntime.dll module - api-ms-win-core-libraryloader-l1-2-0.dll marked a delay load, that causes recursion and stackoverflow.
     * Bug 2: logging callback cannot be shared between multiple components 
     * Bug 3: the same completion callback for MSALRUNTIME_ReadAccountByIdAsync and MSALRUNTIME_SignInAsync it is not clear what should I handle when I read account, which fields are important.
     * Bug 4: we need AcquireToken API that produces both UI and silent call in one shot.
     * Bug 5: Logging callback over complicated and not consistent, we produce object, then with one method read multiple properties from it. I think this prototype will be simpler:
     *          typedef void(MSALRUNTIME_API* MSALRUNTIME_LOG_CALLBACK_ROUTINE)(MSALRUNTIME_LOG_LEVEL logLevel, const char* logEntry, void* callbackData);
     * Bug 6: SignIn must have account hint, like signInInteractively
    */

    public class Core : CriticalFinalizerObject, IDisposable
    {
        private bool _alive = false;
        public Core()
        {
            Module.AddRef();
            _alive = true;
        }

        ~Core()
        {
            Dispose(false);
        }

        /// <summary>
        /// Forces the GC and checks that no handles are still live.
        /// </summary>
        public static void VerifyHandleLeaksForTest()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (Module.HandleCount > 0)
            {
                throw new System.InvalidOperationException($"There are {Module.HandleCount} objects that are not released. Investigate handle leak.");
            }
        }

        public Task<AuthResult> SignInAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.SignInAsync(parentHwnd, authParameters, correlationId, out asyncObj);
            DisposeAsync(asyncObj);
            return result;
        }

        public Task<AuthResult> SignInAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, CancellationToken cancellationToken)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.SignInAsync(parentHwnd, authParameters, correlationId, out asyncObj);
            LinkAsync(asyncObj, cancellationToken);
            return result;
        }

        public Task<AuthResult> SignInSilentlyAsync(AuthParameters authParameters, string correlationId)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.SignInSilentlyAsync(authParameters, correlationId, out asyncObj);
            DisposeAsync(asyncObj);
            return result;
        }

        public Task<AuthResult> SignInSilentlyAsync(AuthParameters authParameters, string correlationId, CancellationToken cancellationToken)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.SignInSilentlyAsync(authParameters, correlationId, out asyncObj);
            LinkAsync(asyncObj, cancellationToken);
            return result;
        }

        public Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, string accountHint = "")
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.SignInInteractivelyAsync(parentHwnd, authParameters, correlationId, accountHint, out asyncObj);
            DisposeAsync(asyncObj);
            return result;
        }

        public Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, CancellationToken cancellationToken)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.SignInInteractivelyAsync(parentHwnd, authParameters, correlationId, "", out asyncObj);
            LinkAsync(asyncObj, cancellationToken);
            return result;
        }

        public Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, string accountHint, CancellationToken cancellationToken)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.SignInInteractivelyAsync(parentHwnd, authParameters, correlationId, accountHint, out asyncObj);
            LinkAsync(asyncObj, cancellationToken);
            return result;
        }

        public Task<Account> ReadAccountByIdAsync(string accountId, string correlationId)
        {
            Async asyncObj;
            Task<Account> result = API.CPU.ReadAccountByIdAsync(accountId, correlationId, out asyncObj);
            DisposeAsync(asyncObj);
            return result;
        }

        public Task<Account> ReadAccountByIdAsync(string accountId, string correlationId, CancellationToken cancellationToken)
        {
            Async asyncObj;
            Task<Account> result = API.CPU.ReadAccountByIdAsync(accountId, correlationId, out asyncObj);
            LinkAsync(asyncObj, cancellationToken);
            return result;
        }

        public Task<AuthResult> AcquireTokenSilentlyAsync(AuthParameters authParameters, string correlationId, Account account)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.AcquireTokenSilentlyAsync(authParameters, correlationId, account, out asyncObj);
            DisposeAsync(asyncObj);
            return result;
        }

        public Task<AuthResult> AcquireTokenSilentlyAsync(AuthParameters authParameters, string correlationId, Account account, CancellationToken cancellationToken)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.AcquireTokenSilentlyAsync(authParameters, correlationId, account, out asyncObj);
            LinkAsync(asyncObj, cancellationToken);
            return result;
        }

        public Task<AuthResult> AcquireTokenInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, Account account)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.AcquireTokenInteractivelyAsync(parentHwnd, authParameters, correlationId, account, out asyncObj);
            DisposeAsync(asyncObj);
            return result;
        }

        public Task<AuthResult> AcquireTokenInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, Account account, CancellationToken cancellationToken)
        {
            Async asyncObj;
            Task<AuthResult> result = API.CPU.AcquireTokenInteractivelyAsync(parentHwnd, authParameters, correlationId, account, out asyncObj);
            LinkAsync(asyncObj, cancellationToken);
            return result;
        }

        public Task<SignOutResult> SignOutSilentlyAsync(string clientId, string correlationId, Account account)
        {
            Async asyncObj;
            Task<SignOutResult> result = API.CPU.SignOutSilentlyAsync(clientId, correlationId, account, out asyncObj);
            DisposeAsync(asyncObj);
            return result;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_alive)
            {
                Module.RemoveRef();
                _alive = false;
            }

            if (disposing)
            {
                GC.SuppressFinalize(this);
            }
        }

        private static void LinkAsync(Async asyncObj, CancellationToken cancellationToken)
        {
            cancellationToken.Register(() =>
            {
                asyncObj.Cancel();
                if (asyncObj != null)
                {
                    asyncObj.Dispose();
                }
            });
        }

        private static void DisposeAsync(Async asyncObj)
        {
            if (asyncObj != null)
            {
                asyncObj.Dispose();
            }
        }

    }

    internal abstract partial class API
    {
        public abstract Task<AuthResult> SignInAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, out Async asyncObj);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInAsync(parentHwnd, authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInAsync(parentHwnd, authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInAsync(parentHwnd, authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInAsync(parentHwnd, authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        public abstract Task<AuthResult> SignInSilentlyAsync(AuthParameters authParameters, string correlationId, out Async asyncObj);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInSilentlyAsync(AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInSilentlyAsync(authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInSilentlyAsync(AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInSilentlyAsync(authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInSilentlyAsync(AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInSilentlyAsync(authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInSilentlyAsync(AuthParameters authParameters, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInSilentlyAsync(authParameters.Handle, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        public abstract Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, string accountHint, out Async asyncObj);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, string accountHint, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, string accountHint, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, accountHint, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, string accountHint, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, string accountHint, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, accountHint, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, string accountHint, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, string accountHint, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, accountHint, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignInInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, string accountHint, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> SignInInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, string accountHint, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignInInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, accountHint, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        protected abstract Task<AuthResult> ReadAccountByIdAsyncInternal(string accountId, string correlationId, out Async asyncObj);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReadAccountByIdAsync(string accountId, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            protected override Task<AuthResult> ReadAccountByIdAsyncInternal(string accountId, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_ReadAccountByIdAsync(accountId, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReadAccountByIdAsync(string accountId, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            protected override Task<AuthResult> ReadAccountByIdAsyncInternal(string accountId, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_ReadAccountByIdAsync(accountId, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReadAccountByIdAsync(string accountId, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            protected override Task<AuthResult> ReadAccountByIdAsyncInternal(string accountId, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_ReadAccountByIdAsync(accountId, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_ReadAccountByIdAsync(string accountId, string correlationId, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            protected override Task<AuthResult> ReadAccountByIdAsyncInternal(string accountId, string correlationId, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_ReadAccountByIdAsync(accountId, correlationId, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        public abstract Task<AuthResult> AcquireTokenSilentlyAsync(AuthParameters authParameters, string correlationId, Account account, out Async asyncObj);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenSilentlyAsync(AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenSilentlyAsync(authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenSilentlyAsync(AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenSilentlyAsync(authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenSilentlyAsync(AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenSilentlyAsync(authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenSilentlyAsync(MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenSilentlyAsync(AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenSilentlyAsync(authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        public abstract Task<AuthResult> AcquireTokenInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, Account account, out Async asyncObj);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_AcquireTokenInteractivelyAsync(IntPtr parentHwnd, MSALRUNTIME_AUTH_PARAMETERS_HANDLE authParameters, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<AuthResult> AcquireTokenInteractivelyAsync(IntPtr parentHwnd, AuthParameters authParameters, string correlationId, Account account, out Async asyncObj)
            {
                return CreateAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_AcquireTokenInteractivelyAsync(parentHwnd, authParameters.Handle, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        public abstract Task<SignOutResult> SignOutSilentlyAsync(string clientId, string correlationId, Account account, out Async asyncObj);

        partial class x86
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignOutSilentlyAsync(string clientId, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<SignOutResult> SignOutSilentlyAsync(string clientId, string correlationId, Account account, out Async asyncObj)
            {
                return CreateSignOutAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignOutSilentlyAsync(clientId, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class x64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignOutSilentlyAsync(string clientId, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<SignOutResult> SignOutSilentlyAsync(string clientId, string correlationId, Account account, out Async asyncObj)
            {
                return CreateSignOutAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignOutSilentlyAsync(clientId, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignOutSilentlyAsync(string clientId, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<SignOutResult> SignOutSilentlyAsync(string clientId, string correlationId, Account account, out Async asyncObj)
            {
                return CreateSignOutAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignOutSilentlyAsync(clientId, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        partial class arm64
        {
            [DllImport(Name, CharSet = CharSet.Unicode)]
            private static extern MSALRUNTIME_ERROR_HANDLE MSALRUNTIME_SignOutSilentlyAsync(string clientId, string correlationId, MSALRUNTIME_ACCOUNT_HANDLE account, MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle);

            public override Task<SignOutResult> SignOutSilentlyAsync(string clientId, string correlationId, Account account, out Async asyncObj)
            {
                return CreateSignOutAsync(
                    (MSALRUNTIME_COMPLETION_ROUTINE callback, IntPtr callbackData, out MSALRUNTIME_ASYNC_HANDLE asyncHandle) =>
                        MSALRUNTIME_SignOutSilentlyAsync(clientId, correlationId, account.Handle, callback, callbackData, out asyncHandle)
                    , out asyncObj);
            }
        }

        public Task<Account> ReadAccountByIdAsync(string accountId, string correlationId, out Async asyncObj)
        {
            return ReadAccountByIdAsyncInternal(accountId, correlationId, out asyncObj)
                .ContinueWith((Task<AuthResult> authResultTask) =>
                {
                    AuthResult authResult = authResultTask.Result;

                    if (authResult.Error != null)
                    {
                        Error error = authResult.Error;

                        // to release resource faster, it is better to dispose it eariler.
                        authResult.Dispose();

                        throw new MsalRuntimeException(error);
                    }

                    Account result = authResult.Account;

                    // to release resource faster, it is better to dispose it eariler.
                    authResult.Dispose();

                    return result;
                });
        }



      

        private static Task<T> CreateAsync<T, U>(MSALAsync asyncFunc, Func<U, T> convert, MSALRUNTIME_COMPLETION_ROUTINE callbackCompletion, out Async asyncObj)
        {
            asyncObj = null;
            TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
            try
            {

                Action<U> callback = (U hResponse) =>
                {
                    tcs.SetResult(convert(hResponse));
                };

                GCHandle gchCallback = GCHandle.Alloc(callback);

                MSALRUNTIME_ASYNC_HANDLE asyncHandle;
                MSALRUNTIME_ERROR_HANDLE hError = asyncFunc(callbackCompletion, GCHandle.ToIntPtr(gchCallback), out asyncHandle);

                if (hError != null)
                {
                    Error error = Error.TryCreate(hError);
                    if (error != null)
                    {
                        gchCallback.Free();
                        tcs.SetException(new MsalRuntimeException(error));
                    }
                }
                else
                {
                    gchCallback.Free();
                    tcs.SetException(new System.Exception("Unexpected behaviour from mid-tier."));
                }

                if (asyncHandle != null)
                {
                    asyncObj = new Async(asyncHandle);
                }

            }
            catch (System.Exception ex)
            {
                tcs.SetException(ex);
            }

            return tcs.Task;
        }

    }

}
