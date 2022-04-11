namespace Microsoft.Identity.Client.NativeInterop
{
    public enum ResponseStatus
    {
        Unexpected = 0,
        Reserved = 1,
        InteractionRequired = 2,
        NoNetwork = 3,
        NetworkTemporarilyUnavailable = 4,
        ServerTemporarilyUnavailable = 5,
        ApiContractViolation = 6,
        UserCanceled = 7,
        ApplicationCanceled = 8,
        IncorrectConfiguration = 9,
        InsufficientBuffer = 10,
        AuthorityUntrusted = 11,
        UserSwitch = 12,
        AccountUnusable = 13
    };

}
