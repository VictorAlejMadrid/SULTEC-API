using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SULTEC_API.Data;
using SULTEC_API.Data.Dtos.UserDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Repositories
{
    public class UserRepository
    {
        private readonly SultecContext _context;
        private readonly IMapper _mapper;

        public UserRepository(SultecContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }

        public async Task<IEnumerable<ReadUserDto>> GetUsersAsync(int pageNumber, int pageSize)
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();

            return _mapper.Map<IEnumerable<ReadUserDto>>(users)
                          .Skip(pageNumber * pageSize)
                          .Take(pageSize);
        }

        public async Task<int> GetUsersCount()
        {
            var totalItems = await _context.Users.CountAsync();

            return totalItems;
        }
    }
}
