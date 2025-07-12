using Betalish.Application.Commands.EmailMessages.SendTestEmail;

namespace Betalish.Web.Pages.Admin.EmailMessages;

public class SendTestEmailModel(
    IUserToken userToken,
    IDatabaseService database,
    ISendTestEmailCommand command) : AdminPageModel(userToken)
{
    [BindProperty]
    public SendTestEmailCommandModel CommandModel { get; set; }

    public IActionResult OnGet()
    {
        try
        {
            AssertIsAdmin();

            CommandModel = new SendTestEmailCommandModel
            {
                FromName = "Betalish",
                FromAddress = "hej@betalish.se",

                Subject = "Testmeddelande",

                HtmlBody = """
                <html>
                <head>
                <title>
                Testmeddelande
                </title>
                </head>
                <body>
                <h1>
                Ett testmeddelande
                <h1>
                </body>
                </html>
                """,

                TextBody = """
                Ett testmeddelande från Betalish.se
                """
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

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-admin-desktop");
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
