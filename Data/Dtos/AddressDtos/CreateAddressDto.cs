using SULTEC_API.Models;

namespace SULTEC_API.Data.Dtos.AdressDtos;

public class CreateAddressDto
{
    public string Street { get; set; }
    public int Number { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? District { get; set; }
    public string City { get; set; }
    public int ClientId { get; set; }

    public static explicit operator Address(CreateAddressDto addressDto)
    {
        return new Address
        {
            ClientId = addressDto.ClientId,
            AdditionalInformation = addressDto.AdditionalInformation,
            District = addressDto.District,
            City = addressDto.City,
            Number = addressDto.Number,
            Street = addressDto.Street,
        };
    }
}
