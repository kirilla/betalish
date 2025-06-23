namespace Betalish.Persistence.Configuration;

class AdminAuthConfiguration : IEntityTypeConfiguration<AdminAuth>
{
    public void Configure(EntityTypeBuilder<AdminAuth> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
