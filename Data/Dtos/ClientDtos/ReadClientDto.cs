using SULTEC_API.Data.Dtos.AddressDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Data.Dtos.ClientDtos;

public class ReadClientDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public virtual ICollection<ReadClientAddressDto> Addresses { get; set; }

    public static explicit operator Client(ReadClientDto clientDto)
    {
        var addressList = new List<Address>();

        if (clientDto.Addresses != null)
        {
            addressList.Concat(clientDto.Addresses.Select(address => (Address)address));
        }

        return new Client
        {
            Id = clientDto.Id,
            Name = clientDto.Name,
            PhoneNumber = clientDto.PhoneNumber,
            Addresses = addressList
        };
    }
}
