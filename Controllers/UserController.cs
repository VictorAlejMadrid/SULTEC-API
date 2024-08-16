using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SULTEC_API.Data;
using SULTEC_API.Data.Dtos.UserDtos;
using SULTEC_API.Models;
using SULTEC_API.Services;

namespace SULTEC_API.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly SultecContext _context;
    private readonly UserService _userService;

    public UserController(IMapper mapper, SultecContext context, UserService userService)
    {
        _mapper = mapper;
        _context = context;
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);

        var result = await _userService.RegisterUser(user, createUserDto.Password);

        if (result.Errors.Any())
            return BadRequest(result.Errors);

        return CreatedAtAction(nameof(GetUserById),
            new { Id = user.Id },
            user);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var result = await _userService.GetUsersAsync(pageNumber, pageSize);

        if (result is null || !result.Any())
            return NotFound();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var result = await _userService.GetUserByIdAsync(id);

        if (result.Errors.Any())
            return NotFound(result.Errors);

        return Ok(result.Data);
    }
}
