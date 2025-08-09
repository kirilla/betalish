namespace Betalish.Persistence.Configuration;

class CustomerMessageConfiguration : IEntityTypeConfiguration<CustomerMessage>
{
    public void Configure(EntityTypeBuilder<CustomerMessage> builder)
    {
        builder.HasKey(p => p.Id);
    }
}
