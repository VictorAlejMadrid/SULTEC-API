using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SULTEC_API.Models;

namespace SULTEC_API.Data;

public class SultecContext : IdentityDbContext<User>
{
    public SultecContext(DbContextOptions<SultecContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Address>()
            .HasOne(address => address.Client)
            .WithMany(client => client.Addresses)
            .HasForeignKey(address => address.ClientId);
    }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Address> Addresses { get; set; }
}
