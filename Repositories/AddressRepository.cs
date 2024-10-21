using Microsoft.EntityFrameworkCore;
using SULTEC_API.Data;
using SULTEC_API.Data.Dtos.AdressDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Repositories;

public class AddressRepository
{
    private readonly SultecContext _context;

    public AddressRepository(SultecContext context)
    {
        _context = context;
    }

    public async Task<bool> AddAddressToDatabase(Address addressDto)
    {
        await _context.Addresses.AddAsync(addressDto);
        var linesAffected = await _context.SaveChangesAsync();

        return linesAffected >= 1;
    }

    public async Task<int> GetAddressCountAsync() => await _context.Addresses.CountAsync();

    public async Task<IEnumerable<ReadAddressDto>> GetAddressesAsync(int pageNumber, int pageSize)
    {
        var addresses = await _context.Addresses
            .AsNoTracking()
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Select(address => (ReadAddressDto)address)
            .ToListAsync();

        return addresses;
    }

    public async Task<Address?> GetAdressByIdAsync(int addressId)
    {
        var address = await _context.Addresses.FirstOrDefaultAsync(address =>  address.Id == addressId);

        return address;
    }

    public async Task<IEnumerable<ReadAddressDto>> GetAddressesByClientIdAsync(int clientId)
    {
        var addresses = await _context.Addresses
            .AsNoTracking()
            .Where(address => address.ClientId == clientId)
            .Select(address => (ReadAddressDto)address)
            .ToListAsync();

        return addresses;
    }

    public async Task<bool> DeleteAddressByIdAsync(int addressId)
    {
        var address = await GetAdressByIdAsync(addressId);

        _context.Addresses.Remove(address);
        var linesAffected = _context.SaveChanges();

        return linesAffected >= 1;
    }
}
