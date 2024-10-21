using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SULTEC_API.Data.DataModel;
using SULTEC_API.Data.Dtos.UserDtos;
using SULTEC_API.Models;
using SULTEC_API.Repositories;

namespace SULTEC_API.Services;

public class UserService 
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _manager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserRepository _userRepository;
    private readonly TokenService _tokenService;

    public UserService(
        IMapper mapper,
        UserManager<User> manager,
        SignInManager<User> signInManager,
        UserRepository userRepository,
        TokenService tokenService)
    {
        _mapper = mapper;
        _manager = manager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<IdentityResult>> RegisterUser(User user, string password)
    {
        var result = new Result<IdentityResult>();

        var operationResult = await _manager.CreateAsync(user, password);

        if (!operationResult.Succeeded)
        {
            result.Code = Int16.Parse(operationResult.Errors.First().Code);
            result.Success = false;
            result.Errors.Add(operationResult.Errors.First().Description);
        }

        result.Data = operationResult;

        return result;
    }

    public async Task<Result<ReadUserDto>> GetUserByIdAsync(string id)
    {
        var result = new Result<ReadUserDto>();

        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null)
        {
            result.Success = false;
            result.Code = 404;
            result.Errors.Add("User not found");
            return result;
        }

        result.Data = _mapper.Map<ReadUserDto>(user);

        return result;
    }

    public async Task<PaginatedResult<IEnumerable<ReadUserDto>>> GetUsersAsync(int pageNumber, int pageSize)
    {
        int totalItems = await _userRepository.GetUsersCount();

        var result = new PaginatedResult<IEnumerable<ReadUserDto>>(pageNumber, pageSize, totalItems);

        var correctPageNumber = pageNumber - 1;

        if (pageSize * correctPageNumber > totalItems)
        {
            result.Code = 400;
            result.Success = false;
            result.Pagination.HasNextPage = false;
            result.Errors.Add("Page number exceds the total number of pages");

            return result;
        }

        var users = await _userRepository.GetUsersAsync(correctPageNumber, pageSize);

        result.Data = _mapper.Map<IEnumerable<ReadUserDto>>(users);

        return result;
    }

    public async Task<Result<string>> Login(LoginUserDto loginUserDto)
    {
        var result = new Result<string>();

        var signResult = await _signInManager
            .PasswordSignInAsync(loginUserDto.UserName, loginUserDto.Password, false, false);

        if (!signResult.Succeeded)
        {
            result.Code = 400;
            result.Success = false;
            result.Errors.Add("Wrong username or password");

            return result;
        }

        var user = _signInManager
            .UserManager.Users.FirstOrDefault(user =>
            user.NormalizedUserName!.Equals(loginUserDto.UserName.ToUpper()));

        var token = _tokenService.GenerateToken(user!);

        result.Data = token;

        return result;
    }

    public async Task<Result<string>> ChangePasswordAsync(ChangePasswordUserDto newPasswordUserDto)
    {
        var result = new Result<string>();

        var user = await _userRepository.GetUserByIdAsync(newPasswordUserDto.Id);

        if (user is null)
        {
            result.Code = 404;
            result.Success = false;
            result.Errors.Add("User not found");

            return result;
        }

        var changeResult = await _manager.ChangePasswordAsync(user,
            newPasswordUserDto.Password,
            newPasswordUserDto.NewPassword);

        if (!changeResult.Succeeded)
        {
            result.Code = Int16.Parse(changeResult.Errors.First().Code);
            result.Success = false;
            result.Errors.Add(changeResult.Errors.First().Description);

            return result;
        }

        return result;
    }
}
