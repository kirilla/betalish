using System.ComponentModel.DataAnnotations;

namespace Betalish.Application.Commands.UserPhones.AddAccountPhone;

public class AddAccountPhoneCommand(IDatabaseService database) : IAddAccountPhoneCommand
{
    public async Task Execute(
        IUserToken userToken, AddAccountPhoneCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        model.Number = model.Number.Trim().StripNonPhoneNumberChars();

        if (await database.UserPhones
            .Where(x => x.UserId == userToken.UserId!.Value)
            .CountAsync() >= Limits.User.PhoneNumbers.Max)
            throw new TooManyException();

        if (await database.UserPhones
            .AnyAsync(x => 
                x.UserId == userToken.UserId!.Value &&
                x.Number == model.Number))
            throw new BlockedByExistingException();

        var phone = new UserPhone()
        {
            UserId = userToken.UserId!.Value,
            Number = model.Number,
        };

        database.UserPhones.Add(phone);

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsAuthenticated;
    }
}
