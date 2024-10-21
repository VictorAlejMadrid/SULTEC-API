using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SULTEC_API.Models;

namespace SULTEC_API.ContextMappings;

public class ClientMapping : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(b => b.Name).IsRequired().HasMaxLength(256);
        builder.Property(b => b.PhoneNumber).IsRequired().HasMaxLength(16);
    }
}
