using Betalish.Application.Commands.Batches.EditBatch;

namespace Betalish.Web.Pages.Clients.Batches;

public class EditBatchModel(
    IUserToken userToken,
    IDatabaseService database,
    IEditBatchCommand command) : ClientPageModel(userToken)
{
    public Batch Batch { get; set; } = null!;

    [BindProperty]
    public EditBatchCommandModel CommandModel { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Batch = await database.Batches
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            CommandModel = new EditBatchCommandModel()
            {
                Id = Batch.Id,
                Name = Batch.Name,
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        try
        {
            if (!command.IsPermitted(UserToken))
                throw new NotPermittedException();

            Batch = await database.Batches
                .Where(x =>
                    x.ClientId == UserToken.ClientId!.Value &&
                    x.Id == id)
                .SingleOrDefaultAsync() ??
                throw new NotFoundException();

            if (!ModelState.IsValid)
                return Page();

            await command.Execute(UserToken, CommandModel);

            return Redirect("/show-batches");
        }
        catch (BlockedByExistingException)
        {
            ModelState.AddModelError(
                nameof(CommandModel.Name),
                "Det finns en annan batch med samma namn.");

            return Page();
        }
        catch
        {
            return Redirect("/help/notpermitted");
        }
    }
}
