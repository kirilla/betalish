namespace Betalish.Persistence.Configuration;

class SessionActivityConfiguration : IEntityTypeConfiguration<SessionActivity>
{
    public void Configure(EntityTypeBuilder<SessionActivity> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
