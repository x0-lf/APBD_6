using APBD_6.Data;
using APBD_6.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_6.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;

    public ClientRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> ClientExistsAsync(int idClient)
    {
        return await _db.Clients.AnyAsync(c => c.IdClient == idClient);
    }

    public async Task<bool> ClientHasTripsAsync(int idClient)
    {
        return await _db.Client_Trips.AnyAsync(ct => ct.IdClient == idClient);
    }
    
    public async Task DeleteClientAsync(int idClient)
    {
        var trips = await _db.Client_Trips
            .Where(ct => ct.IdClient == idClient)
            .ToListAsync();

        if (trips.Any())
        {
            _db.Client_Trips.RemoveRange(trips);
        }

        var client = await _db.Clients.FindAsync(idClient);
        if (client is not null)
        {
            _db.Clients.Remove(client);
        }

        await _db.SaveChangesAsync();
    }


    public async Task<bool> ClientWithPeselExistsAsync(string pesel)
    {
        return await _db.Clients.AnyAsync(c => c.Pesel == pesel);
    }

    public async Task<Client_Trip?> GetClientTripAsync(int idTrip, string pesel)
    {
        return await _db.Client_Trips
            .Include(ct => ct.IdClientNavigation)
            .FirstOrDefaultAsync(ct =>
                ct.IdTrip == idTrip &&
                ct.IdClientNavigation != null &&
                ct.IdClientNavigation.Pesel == pesel);
    }

    public async Task AddClientWithTripAsync(Client client, Client_Trip clientTrip)
    {
        await _db.Clients.AddAsync(client);
        await _db.Client_Trips.AddAsync(clientTrip);
        await _db.SaveChangesAsync();
    }
}