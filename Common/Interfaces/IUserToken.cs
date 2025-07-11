namespace Betalish.Common.Interfaces;

public interface IUserToken
{
    int? UserId { get; }
    int? SessionId { get; }

    string? Name { get; }

    bool IsAdmin { get; }
    bool IsAuthenticated { get; }
    bool IsClient { get; }

    int? ClientId { get; }
    string? ClientName { get; }

    bool NoLogin { get; }
    bool NoSave { get; set; }
}

// NOTE:
//
// The NoSave setter is on purpose, to allow
// for it to be overriden, on occassions
// when it may be necessary, e.g. in the 
// SelectClient command. Without this,
// (or something else to the same effect),
// a user would not be able to enter the client, 
// and the read-only access would be moot.
//
// Turning off a user's NoSave boolean for a
// single command, e.g. SelectClient, should
// have no ill effects as long as the nature
// of a UserToken is single-shot, and its lifetime
// only the duration of that single request.
