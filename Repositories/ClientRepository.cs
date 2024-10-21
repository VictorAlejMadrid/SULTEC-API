using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SULTEC_API.Data;
using SULTEC_API.Data.Dtos.AddressDtos;
using SULTEC_API.Data.Dtos.ClientDtos;
using SULTEC_API.Models;

namespace SULTEC_API.Repositories;

public class ClientRepository
{
    private readonly SultecContext _context;
    private readonly IMemoryCache _memoryCache;

    public ClientRepository(SultecContext context, IMemoryCache memoryCache)
    {
        _context = context;
        _memoryCache = memoryCache;
    }
    public async Task<Client> AddClientToDatabaseAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        var clientDto = (ReadClientDto)client;
    
        return client;
    }

    public async Task<Client?> GetClientAsync(int clientId)
    {
        var client = await _context.Clients
            .AsNoTracking()
            .Include(client => client.Addresses)
            .FirstOrDefaultAsync(client => client.Id == clientId);

        return client;
    }

    public async Task<IEnumerable<ReadClientDto>> GetClientsAsync(int pageNumber, int pageSize)
    {
        var clients = await _context.Clients
            .AsNoTracking()
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(client => client.Addresses)
            .Select(client => (ReadClientDto)client)
            .ToListAsync();

        return clients;
    }

    public async Task<int> GetClientsCountAsync() => await _context.Clients.AsNoTracking().CountAsync();

    public async Task<IEnumerable<ReadClientDto>> GetClientsByStreetName(int pageNumber, int pageSize, string street)
    {
        var addressIds = await _context.Addresses
            .AsNoTracking()
            .Where(address => address.Street.Contains(street))
            .Select(address => address.ClientId)
            .ToListAsync();

        var clients = await _context.Clients
            .AsNoTracking()
            .Where(client => addressIds.Contains(client.Id))
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Include(client => client.Addresses)
            .Select(client => (ReadClientDto)client)
            .ToListAsync();

        int totalCount = clients.Count();

        _memoryCache.Set("TotalClientsByStreetNameCount", totalCount, TimeSpan.FromSeconds(5));

        return clients ?? [];
    }

    public async Task<bool> RemoveClientFromDatabase(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(client => client.Id == id);

        if (client == null)
        {
            return false;
        }

        _context.Clients.Remove(client);
        var linesAffected = await _context.SaveChangesAsync();

        return linesAffected >= 1;
    }
}
