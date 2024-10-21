using SULTEC_API.Data.Dtos.AdressDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Data.Dtos.ClientDtos;

public class CreateClientDto
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public CreateAddressDto? Address { get; set; }

    public static explicit operator Client(CreateClientDto clientDto)
    {
        var addressList = new List<Address>();

        if (clientDto.Address != null) 
        {
            addressList.Add((Address)clientDto.Address);
        }

        return new Client 
        {
            Name = clientDto.Name,
            PhoneNumber = clientDto.PhoneNumber,
            Addresses = addressList
        };
    }
}
