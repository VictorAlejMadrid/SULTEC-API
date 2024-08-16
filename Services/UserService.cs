using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SULTEC_API.Data.Dtos;
using SULTEC_API.Data.Dtos.UserDtos;
using SULTEC_API.Models;
using SULTEC_API.Repositories.Interfaces;

namespace SULTEC_API.Services;

public class UserService : ServiceBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _manager;
    private readonly SignInManager<User> _signInManager;
    private readonly IUserRepository _userRepository;

    public UserService(
        IMapper mapper,
        UserManager<User> manager,
        SignInManager<User> signInManager,
        IUserRepository userRepository)
    {
        _mapper = mapper;
        _manager = manager;
        _signInManager = signInManager;
        _userRepository = userRepository;
    }

    public async Task<Result> RegisterUser(User user, string password)
    {
        var result = await _manager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            Result.Success = false;
            Result.Errors.Add(result.Errors.First().Description);
        }

        return Result;
    }

    public async Task<Result> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            Result.Errors.Add("Usuário não encontrado");
            return Result;
        }

        Result.Data = user;

        return Result;
    }

    public async Task<IEnumerable<ReadUserDto>> GetUsersAsync(int pageNumber, int pageSize)
    {
        return await _userRepository.GetUsersAsync(pageNumber, pageSize);
    }
}
