using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SULTEC_API.Models;

namespace SULTEC_API.ContextMappings;

public class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(b => b.Street).IsRequired().HasMaxLength(100);
        builder.Property(b => b.Number).IsRequired();
        builder.Property(b => b.City).IsRequired().HasMaxLength(50);
        builder.Property(b => b.District).HasMaxLength(50);
        builder.Property(b => b.AdditionalInformation).HasMaxLength(50);
        builder.Property(b => b.Client);
    }
}
