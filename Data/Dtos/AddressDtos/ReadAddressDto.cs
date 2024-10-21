using SULTEC_API.Data.Dtos.ClientDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Data.Dtos.AdressDtos;

public class ReadAddressDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? District { get; set; }
    public string City { get; set; }
    public int ClientId { get; set; }
    public virtual ReadClientDto Client { get; set; }

    public static explicit operator Address(ReadAddressDto addressDto)
    {
        return new Address
        {
            Id = addressDto.Id,
            AdditionalInformation = addressDto.AdditionalInformation,
            District = addressDto.District,
            City = addressDto.City,
            ClientId = addressDto.ClientId,
            Number = addressDto.Number,
            Street = addressDto.Street,
            Client = (Client)addressDto.Client,
        };
    }
}
