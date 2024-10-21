using SULTEC_API.Data.Dtos.AddressDtos;
using SULTEC_API.Data.Dtos.AdressDtos;
using SULTEC_API.Data.Dtos.ClientDtos;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace SULTEC_API.Models;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? District { get; set; }
    public string City { get; set; }
    public int ClientId { get; set; }
    public virtual Client? Client { get; set; }

    public static explicit operator ReadClientAddressDto(Address address)
    {
        return new ReadClientAddressDto
        {
            Id = address.Id,
            Street = address.Street,
            Number = address.Number,
            AdditionalInformation = address.AdditionalInformation,
            District = address.District,
            City = address.City
        };
    }

    public static explicit operator ReadAddressDto(Address address) 
    {
        return new ReadAddressDto
        {
            Id = address.Id,
            Street = address.Street,
            Number = address.Number,
            AdditionalInformation = address.AdditionalInformation,
            District = address.District,
            City = address.City,
            ClientId = address.ClientId,
            Client = address.Client == null ? null : (ReadClientDto)address.Client
        };
    }
}
