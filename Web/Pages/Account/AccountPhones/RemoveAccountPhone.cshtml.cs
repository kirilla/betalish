using Betalish.Application.Commands.UserPhones.RemoveAccountPhone;

namespace Betalish.Web.Pages.Account.AccountPhones;

public class RemoveAccountPhoneModel(
    IUserToken userToken,
    IDatabaseService database,
    IRemoveAccountPhoneCommand command) : UserTokenPageModel(userToken)
{
    public List<UserPhone> UserPhones { get; set; }

    [BindProperty]
    public RemoveAccountPhoneCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            UserPhones = await database.UserPhones
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            CommandModel = new RemoveAccountPhoneCommandModel();

            return Page();
        }
        catch (NotFoundException)
        {
            return Redirect("/help/notfound");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            UserPhones = await database.UserPhones
                .Where(x => x.UserId == UserToken.UserId!.Value)
                .ToListAsync();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect($"/show-account");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
