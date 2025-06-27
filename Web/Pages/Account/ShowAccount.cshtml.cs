using Betalish.Application.Commands.Account.ChangePassword;
using Betalish.Application.Commands.Account.DeleteAccount;
using Betalish.Application.Commands.Account.EditAccount;
using Betalish.Application.Commands.UserEmails.AddUserEmail;
using Betalish.Application.Commands.UserEmails.RemoveUserEmail;
using Betalish.Application.Commands.UserPhones.AddAccountPhone;
using Betalish.Application.Commands.UserPhones.RemoveAccountPhone;

namespace Betalish.Web.Pages.Account;

public class ShowAccountModel(
    IDatabaseService database,
    IUserToken userToken,
    IChangePasswordCommand changePasswordCommand,
    IDeleteAccountCommand deleteAccountCommand,
    IEditAccountCommand editAccountCommand,
    IAddUserEmailCommand addUserEmailCommand,
    IRemoveUserEmailCommand removeUserEmailCommand,
    IAddAccountPhoneCommand addAccountPhoneCommand,
    IRemoveAccountPhoneCommand removeAccountPhoneCommand) : UserTokenPageModel(userToken)
{
    public new User User { get; set; }

    public List<UserEmail> UserEmails { get; set; }
    public List<UserPhone> UserPhones { get; set; }

    public bool CanChangePassword { get; set; }
    public bool CanDeleteAccount { get; set; }
    public bool CanEditAccount { get; set; }
    public bool CanAddUserEmail { get; set; }
    public bool CanRemoveUserEmail { get; set; }
    public bool CanAddAccountPhone { get; set; }
    public bool CanRemoveAccountPhone { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!UserToken.IsAuthenticated)
                throw new NotPermittedException();

            User = await database.Users
                .AsNoTracking()
                .Where(x => x.Id == UserToken.UserId!.Value)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            UserEmails = await database.UserEmails
                .AsNoTracking()
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            UserPhones = await database.UserPhones
                .AsNoTracking()
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            CanChangePassword = changePasswordCommand.IsPermitted(userToken);
            CanDeleteAccount = deleteAccountCommand.IsPermitted(userToken);
            CanEditAccount = editAccountCommand.IsPermitted(userToken);

            CanAddUserEmail =
                addUserEmailCommand.IsPermitted(userToken) &&
                UserEmails.Count < Limits.User.EmailAddresses.Max;

            CanRemoveUserEmail = removeUserEmailCommand.IsPermitted(userToken);

            CanAddAccountPhone =
                addAccountPhoneCommand.IsPermitted(userToken) &&
                UserPhones.Count < Limits.User.PhoneNumbers.Max;

            CanRemoveAccountPhone = removeAccountPhoneCommand.IsPermitted(userToken);

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
