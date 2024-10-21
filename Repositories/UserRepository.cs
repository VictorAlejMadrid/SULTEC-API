using Microsoft.EntityFrameworkCore;
using SULTEC_API.Data;
using SULTEC_API.Models;

namespace SULTEC_API.Repositories;

public class UserRepository
{
    private readonly SultecContext _context;

    public UserRepository(SultecContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

        return user;
    }

    public async Task<IEnumerable<User>> GetUsersAsync(int pageNumber, int pageSize)
    {
        var users = await _context.Users
            .AsNoTracking()
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return users;
    }

    public async Task<int> GetUsersCount() => await _context.Users.CountAsync();
}
