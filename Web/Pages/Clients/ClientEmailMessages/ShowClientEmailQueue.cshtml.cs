namespace Betalish.Web.Pages.Clients.ClientEmailMessages;

public class ShowClientEmailQueueModel(
    IUserToken userToken,
    IDatabaseService database) : ClientPageModel(userToken)
{
    public List<EmailHeader> EmailHeaders { get; set; }

    public int NotSent { get; set; }
    public int Failed { get; set; }
    public int Sent { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            await AssertClientAuthorization(database);

            EmailHeaders = await database.ClientEmailMessages
                .Where(x => x.ClientId == UserToken.ClientId!.Value)
                .OrderBy(x => x.Created)
                .Select(x => new EmailHeader()
                {
                    Id = x.Id,
                    ToName = x.ToName,
                    ToAddress = x.ToAddress,
                    FromName = x.FromName,
                    FromAddress = x.FromAddress,
                    ReplyToName = x.ReplyToName,
                    ReplyToAddress = x.ReplyToAddress,
                    Subject = x.Subject,
                    EmailStatus = x.EmailStatus,
                    Created = x.Created,
                    Sent = x.Sent,
                })
                .ToListAsync();

            NotSent = EmailHeaders.Count(x => x.EmailStatus == EmailStatus.NotSent);
            Failed = EmailHeaders.Count(x => x.EmailStatus == EmailStatus.SendFailed);
            Sent = EmailHeaders.Count(x => x.EmailStatus == EmailStatus.Sent);

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
}
