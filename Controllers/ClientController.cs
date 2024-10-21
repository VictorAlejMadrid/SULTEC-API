using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SULTEC_API.Data.DataModel;
using SULTEC_API.Data.Dtos.ClientDtos;
using SULTEC_API.Models;
using SULTEC_API.Services;

namespace SULTEC_API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientController(IMapper mapper, ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost]
    public async Task<IActionResult> PostClient([FromBody] CreateClientDto clientDto)
    {
        var result = await _clientService.PostClientAsync(clientDto);

        if (result.Errors.Any())
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetClientById),
                new { id = result.Data!.Id },
                result.Data);
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _clientService.GetClientByIdAsync(id);

        if (client.Errors.Any())
        {
            return BadRequest(client);
        }

        return Ok(client);
    }

    [HttpGet]
    public async Task<IActionResult> GetClients([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? street)
    {
        if (street == null)
        {
            var clients = await _clientService.GetPaginatedClientsAsync(pageNumber, pageSize);

            if (clients.Errors.Any())
            {
                return BadRequest(clients);
            }

            return Ok(clients);
        }

        var streetClients = await _clientService.GetPaginatedClientsByStreetAsync(pageSize, pageNumber, street);

        return Ok(streetClients);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(string id)
    {
        var result = await _clientService.DeleteClientAsync(id);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return NoContent();
    }
}
