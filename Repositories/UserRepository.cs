using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SULTEC_API.Data;
using SULTEC_API.Data.Dtos.UserDtos;
using SULTEC_API.Repositories.Interfaces;

namespace SULTEC_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SultecContext _context;
        private readonly IMapper _mapper;

        public UserRepository(SultecContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadUserDto> GetUserByIdAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

            return _mapper.Map<ReadUserDto>(user);
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
