using SULTEC_API.Data.Dtos.UserDtos;

namespace SULTEC_API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<ReadUserDto> GetUserByIdAsync(string id);
        public Task<IEnumerable<ReadUserDto>> GetUsersAsync(int pageNumber, int pageSize);
    }
}
