namespace Betalish.Domain.Enums;

public enum LogItemKind
{
    Assertion = 621921758,

    NetworkRequestReaped = 644146724,
    NetworkRequestReaperFailed = 997229645,

    SignupsReaped = 786751720,

    TerminateSessions = 661471773,

    IpAddressRateLimited = 820643908,
    SignInRateLimited = 864697948,
    EndpointRateLimited = 291390178,

    SetInvoiceNumberRoutineFailed = 331095278,

    Test = 94729107,
}
