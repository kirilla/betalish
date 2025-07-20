namespace Betalish.Application.Commands.InvoiceRanges.EditInvoiceRange;

public class EditInvoiceRangeCommand(IDatabaseService database) : IEditInvoiceRangeCommand
{
    public async Task Execute(
        IUserToken userToken, EditInvoiceRangeCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var range = await database.InvoiceRanges
            .Where(x =>
                x.ClientId == userToken.ClientId!.Value &&
                x.Id == model.Id)
            .SingleOrDefaultAsync() ??
            throw new NotFoundException();

        var startDate = model.StartDate!.TryParseDate()!.ToDateOnly();
        var endDate = model.EndDate!.TryParseDate()!.ToDateOnly();

        var ranges = await database.InvoiceRanges
            .AsNoTracking()
            .Where(x => 
                x.ClientId == userToken.ClientId!.Value &&
                x.Id != model.Id)
            .ToListAsync();

        if (ranges.Any(x =>
            x.ContainsDate(startDate ?? DateOnly.MinValue) ||
            x.ContainsDate(endDate ?? DateOnly.MaxValue)))
            throw new BlockedByDateException();

        if (ranges.Any(x =>
            x.ContainsNumber(model.StartNumber) ||
            x.ContainsNumber(model.EndNumber)))
            throw new BlockedByNumberException();

        range.StartNumber = model.StartNumber;
        range.EndNumber = model.EndNumber;
        range.Comment = model.Comment!;
        range.StartDate = startDate;
        range.EndDate = endDate;

        await database.SaveAsync(userToken);
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
