namespace Betalish.Application.Commands.InvoiceRanges.AddInvoiceRange;

public class AddInvoiceRangeCommand(IDatabaseService database) : IAddInvoiceRangeCommand
{
    public async Task<int> Execute(
        IUserToken userToken, AddInvoiceRangeCommandModel model)
    {
        if (!IsPermitted(userToken))
            throw new NotPermittedException();

        model.TrimStringProperties();
        model.SetEmptyStringsToNull();

        var startDate = model.StartDate!.TryParseDate()!.ToDateOnly();
        var endDate = model.EndDate!.TryParseDate()!.ToDateOnly();

        var ranges = await database.InvoiceRanges
            .AsNoTracking()
            .Where(x => x.ClientId == userToken.ClientId!.Value)
            .ToListAsync();

        if (ranges.Any(x =>
            x.ContainsDate(startDate ?? DateOnly.MinValue) ||
            x.ContainsDate(endDate ?? DateOnly.MaxValue)))
            throw new BlockedByDateException();

        if (ranges.Any(x =>
            x.ContainsNumber(model.StartNumber) ||
            x.ContainsNumber(model.EndNumber)))
            throw new BlockedByNumberException();

        var range = new InvoiceRange()
        {
            StartNumber = model.StartNumber,
            EndNumber = model.EndNumber,
            Comment = model.Comment,
            StartDate = startDate,
            EndDate = endDate,
            ClientId = userToken.ClientId!.Value,
        };

        database.InvoiceRanges.Add(range);

        await database.SaveAsync(userToken);

        return range.Id;
    }

    public bool IsPermitted(IUserToken userToken)
    {
        return userToken.IsClient;
    }
}
