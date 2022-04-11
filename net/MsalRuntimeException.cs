namespace Microsoft.Identity.Client.NativeInterop
{
    public class MsalRuntimeException : System.Exception
    {
        Error _result;
        public MsalRuntimeException(Error result)
            : base(result.ToString())
        {
            _result = result;
        }

        public ResponseStatus Status => _result.Status;
        public int ErrorCode => _result.ErrorCode;
        public int Tag => _result.Tag;
        public string Context => _result.Context;

    }

}
