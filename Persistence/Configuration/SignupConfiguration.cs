namespace Betalish.Persistence.Configuration;

class SignupConfiguration : IEntityTypeConfiguration<Signup>
{
    public void Configure(EntityTypeBuilder<Signup> builder)
    {
        builder.HasKey(p => p.Id);

        // Person
        builder.Property(p => p.Ssn12)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Ssn.Ssn12);

        builder.Property(p => p.PersonName)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Person.Name);

        builder.Property(p => p.EmailAddress)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Email.Address);

        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(MaxLengths.Common.Phone.Number);
    }
}
