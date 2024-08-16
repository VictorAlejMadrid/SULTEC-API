using Microsoft.EntityFrameworkCore;
using SULTEC_API.Controllers;
using SULTEC_API.Models;

namespace SULTEC_API.Data;

public class SultecContext : DbContext
{
    public SultecContext(DbContextOptions<SultecContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
}
