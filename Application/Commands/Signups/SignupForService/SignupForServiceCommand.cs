using Betalish.Application.EmailTemplates.Signups;
using Betalish.Common.Settings;
using Microsoft.Extensions.Options;

namespace Betalish.Application.Commands.Signups.SignupForService;

public class SignupForServiceCommand(
    IDatabaseService database,
    ISignupEmailTemplate emailTemplate,
    IOptions<SignUpConfiguration> options) : ISignupForServiceCommand
{
    private readonly SignUpConfiguration _config = options.Value;

    public async Task Execute(
        IUserToken userToken,
        SignupForServiceCommandModel model)
    {
        if (!_config.AllowSignupForService)
            throw new FeatureTurnedOffException();

        if (userToken.IsAuthenticated)
            throw new PleaseLogOutException();

        if (!IsPermitted(userToken))
            throw new NotPermittedException();
        // NOTE: Redundant, for uniformity.

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.EmailAddress = model.EmailAddress.ToLowerInvariant();
        model.Ssn12 = model.Ssn12.StripNonNumeric();

        if (!SsnService.IsValidSsn(model.Ssn12))
            throw new InvalidSsnException();

        // Blocking data or conflicts?
        var ssn10 = model.Ssn12.ToSsn10();

        if (await database.Users.AnyAsync(x => x.Ssn10 == ssn10))
            throw new BlockedBySsnException();

        if (await database.UserEmails.AnyAsync(x => x.Address == model.EmailAddress))
            throw new BlockedByEmailException();

        // Create the signup
        var signup = new Signup()
        {
            Guid = Guid.NewGuid(),
            Ssn12 = model.Ssn12,
            PersonName = model.PersonName,
            EmailAddress = model.EmailAddress,
            PhoneNumber = model.PhoneNumber,
        };

        // Guid Sanity-checking
        if (signup.Guid == Guid.Empty)
            throw new Exception("Empty Guid value");

        if (await database.Signups.AnyAsync(x => x.Guid == signup.Guid))
            throw new Exception("Guid collision.");

        database.Signups.Add(signup);

        // Email notification
        var message = emailTemplate.Create(signup);

        database.EmailMessages.Add(message);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return !userToken.IsAuthenticated;
    }
}
