using Betalish.Application.Commands.EmailMessages.SendEmail;

namespace Betalish.Web.Pages.Admin.EmailMessages;

public class SendEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendEmailCommand command) : AdminPageModel(userToken)
{
    public EmailMessage EmailMessage { get; set; }

    [BindProperty]
    public SendEmailCommandModel CommandModel { get; set; }

    public async Task<IActionResult> OnGetAsync(int emailMessageId)
    {
        try
        {
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailMessage = await database.EmailMessages
                .Where(x => x.Id == emailMessageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new SendEmailCommandModel()
            {
                EmailMessageId = emailMessageId,
            };

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
            AssertIsAdmin();

            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            EmailMessage = await database.EmailMessages
                .Where(x => x.Id == CommandModel.EmailMessageId)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-attacks");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
