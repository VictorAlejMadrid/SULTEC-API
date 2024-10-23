using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using SULTEC_API.Data.DataModel;
using SULTEC_API.Data.Dtos.ClientDtos;
using SULTEC_API.Models;
using SULTEC_API.Repositories;
using System.Collections.Generic;
using System.Net;

namespace SULTEC_API.Services;

public class ClientService
{
    private readonly ClientRepository _clientRepository;
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _memoryCache;

    public ClientService(IConfiguration configuration, ClientRepository clientRepository, IMemoryCache memoryCache)
    {
        _configuration = configuration;
        _clientRepository = clientRepository;
        _memoryCache = memoryCache;
    }
    public async Task<Result<Client>> PostClientAsync(CreateClientDto clientDto)
    {
        var result = new Result<Client>();

        var client = (Client)clientDto;

        var createdClient = await _clientRepository.AddClientToDatabaseAsync(client);

        if (createdClient == null)
        {
            result.Code = 404;
            result.Success = false;
            result.Errors.Add("An error has ocurred");

            return result;
        }

        result.Data = createdClient;

        return result;
    }

    public async Task<Result<ReadClientDto>> GetClientByIdAsync(int clientId)
    {
        var result = new Result<ReadClientDto>();

        var client = await _clientRepository.GetClientAsync(clientId);

        if (client == null)
        {
            result.Code = 404;
            result.Success = false;
            result.Errors.Add("Client not found");

            return result;
        }

        result.Data = (ReadClientDto)client;

        return result;
    }

    public async Task<PaginatedResult<IEnumerable<ReadClientDto>>> GetPaginatedClientsAsync(int pageNumber, int pageSize)
    {
        var totalItems = await _clientRepository.GetClientsCountAsync();
        var result = new PaginatedResult<IEnumerable<ReadClientDto>>(pageNumber, pageSize, totalItems);

        var correctPageNumber = pageNumber - 1;

        if (pageSize * correctPageNumber > totalItems)
        {
            result.Success = false;
            result.Code = HttpStatusCode.BadRequest.GetHashCode();
            result.Pagination.HasNextPage = false;
            result.Errors.Add("Page number exceds the total number of pages");

            return result;
        }
        var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
        
        var clients = await _clientRepository.GetClientsAsync(correctPageNumber, pageSize);

        result.Pagination.HasNextPage = pageNumber < totalPages;
        result.Pagination.TotalPages = totalPages;
        result.Data = clients; 

        return result;
    }

    public async Task<Result<bool>> DeleteClientAsync(string id)
    {
        var result = new Result<bool>();

        var intId = int.TryParse(id, out int idConverted);

        if (!intId)
        {
            result.Success = false;
            result.Errors.Add("Invalid Id");
            result.Code = HttpStatusCode.BadRequest.GetHashCode();

            return result;
        };

        var repositoryResult = await _clientRepository.RemoveClientFromDatabase(idConverted);

        if (!repositoryResult)
        {
            result.Errors.Add("User not found");
            result.Success = false;
            result.Code = HttpStatusCode.NotFound.GetHashCode();

            return result;
        }

        result.Data = repositoryResult;

        return result;
    }

    public async Task<Result<IEnumerable<ReadClientDto>>> GetPaginatedClientsByStreetAsync(int pageSize, int pageNumber, string street)
    {
        int correctPageNumber = pageNumber - 1;

        var clients = await _clientRepository.GetClientsByStreetName(correctPageNumber, pageSize, street);

        var totalcount = _memoryCache.Get("TotalClientsByStreetNameCount");
        var parseResult = int.TryParse(totalcount!.ToString(), out int totalItems);

        var result = new PaginatedResult<IEnumerable<ReadClientDto>>(pageNumber, pageSize, totalItems);

        if (pageSize * correctPageNumber > totalItems)
        {
            result.Success = false;
            result.Code = HttpStatusCode.BadRequest.GetHashCode();
            result.Pagination.HasNextPage = false;
            result.Errors.Add("Page number exceds the total number of pages");

            return result;
        }
        var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);

        result.Pagination.HasNextPage = pageNumber < totalPages;
        result.Pagination.TotalPages = totalPages;
        result.Data = clients;

        return result;
    }
}
