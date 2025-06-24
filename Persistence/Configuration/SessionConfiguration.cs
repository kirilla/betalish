namespace Betalish.Persistence.Configuration;

class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(x => x.SessionActivities)
            .WithOne(x => x.Session)
            .HasForeignKey(x => x.SessionId);
    }
}
