using AutoMapper;
using SULTEC_API.Data.DataModel;
using SULTEC_API.Data.Dtos.AdressDtos;
using SULTEC_API.Data.Dtos.ClientDtos;
using SULTEC_API.Models;
using SULTEC_API.Repositories;
using System.Net;

namespace SULTEC_API.Services;

public class AddressService
{
    private readonly AddressRepository _repository;

    public AddressService(AddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ReadAddressDto>> PostAddress(CreateAddressDto addressDto)
    {
        var result = new Result<ReadAddressDto>();

        var address = (Address)addressDto;
        var repositoryResult = await _repository.AddAddressToDatabase(address);

        if (!repositoryResult)
        {
            result.Success = false;
            result.Errors.Add("An error has ocurred while adding the address to database");
            result.Code = HttpStatusCode.BadRequest.GetHashCode();

            return result;
        }

        result.Code = HttpStatusCode.Created.GetHashCode();
        result.Data = (ReadAddressDto)address;

        return result;
    }

    public async Task<Result<IEnumerable<ReadAddressDto>>> GetPaginatedAdresses(int pageSize, int pageNumber)
    {
        var totalItems = await _repository.GetAddressCountAsync();
        var result = new PaginatedResult<IEnumerable<ReadAddressDto>>(pageNumber, pageSize, totalItems);

        var correctPageNumber = pageNumber - 1;

        if (pageSize * correctPageNumber > totalItems)
        {
            result.Code = HttpStatusCode.BadRequest.GetHashCode();
            result.Success = false;
            result.Pagination.HasNextPage = false;
            result.Errors.Add("Page number exceds the total number of pages");

            return result;
        }

        result.Data = await _repository.GetAddressesAsync(correctPageNumber, pageSize);

        return result;
    }

    public async Task<Result<IEnumerable<ReadAddressDto>>> GetAddressesByClientId(int clientId)
    {
        var result = new Result<IEnumerable<ReadAddressDto>>();

        result.Data = await _repository.GetAddressesByClientIdAsync(clientId);

        return result;
    }

    public async Task<Result<bool>> DeleteAddressByIdAsync(int addressId)
    {
        var result = new Result<bool>();

        var repositoryResult = await _repository.DeleteAddressByIdAsync(addressId);

        if (!repositoryResult)
        {
            result.Success = false;
            result.Errors.Add("Address not found");
            result.Code= HttpStatusCode.NotFound.GetHashCode();

            return result;
        }

        result.Data = repositoryResult;
        return result;
    }
}
