using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SULTEC_API.Data.Dtos.AdressDtos;
using SULTEC_API.Services;

namespace SULTEC_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly AddressService _addressService;

    public AddressController(AddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAddress(CreateAddressDto addressDto)
    {
        var result = await _addressService.PostAddress(addressDto);
        
        if (result.Errors.Any())
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAddresses([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var result = await _addressService.GetPaginatedAdresses(pageSize, pageNumber);

        if (result.Errors.Any())
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("{clientId}")]
    public async Task<IActionResult> GetAddressesByClientId(int clientId)
    {
        var result = await _addressService.GetAddressesByClientId(clientId);

        return Ok(result);
    }

    [HttpDelete("{addressId}")]
    public async Task<IActionResult> DeleteAddressById(int addressId)
    {
        var result = await _addressService.DeleteAddressByIdAsync(addressId);

        if (result.Errors.Any())
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
