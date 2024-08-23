using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SULTEC_API.Models;

namespace SULTEC_API.Data;

public class SultecContext : IdentityDbContext<User>
{
    public SultecContext(DbContextOptions<SultecContext> options) : base(options) { }
}
