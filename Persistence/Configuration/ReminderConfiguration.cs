namespace Betalish.Persistence.Configuration;

class ReminderConfiguration : IEntityTypeConfiguration<Reminder>
{
    public void Configure(EntityTypeBuilder<Reminder> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
