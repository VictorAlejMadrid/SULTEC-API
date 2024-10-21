using SULTEC_API.Models;

namespace SULTEC_API.Data.Dtos.AddressDtos;

public class ReadClientAddressDto
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string? AdditionalInformation { get; set; }
    public string? District { get; set; }
    public string City { get; set; }
    public int ClientId {  get; set; }

    public static explicit operator Address(ReadClientAddressDto addressDto)
    {
        return new Address
        {
            Id = addressDto.Id,
            AdditionalInformation = addressDto.AdditionalInformation,
            City = addressDto.City,
            Street = addressDto.Street,
            Number = addressDto.Number,
            District = addressDto.District,
            ClientId = addressDto.ClientId
        };
    }
}
