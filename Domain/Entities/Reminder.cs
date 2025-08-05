namespace Betalish.Domain.Entities;

public class Reminder
{
    public int Id { get; set; }

    public DateOnly? Date { get; set; }

    // Relations
    public Invoice Invoice { get; set; } = null!;
}
