namespace Betalish.Persistence.Configuration;

class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
