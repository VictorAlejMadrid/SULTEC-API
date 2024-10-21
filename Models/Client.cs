using SULTEC_API.Data.Dtos.AddressDtos;
using SULTEC_API.Data.Dtos.AdressDtos;
using SULTEC_API.Data.Dtos.ClientDtos;

namespace SULTEC_API.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public virtual ICollection<Address> Addresses { get; set; }

    public static explicit operator ReadClientDto(Client client)
    {
        return new ReadClientDto
        {
            Id = client.Id,
            Name = client.Name,
            PhoneNumber = client.PhoneNumber,
            Addresses = client.Addresses.Select(address => 
                (ReadClientAddressDto)address).ToList()
        };
    }
}
