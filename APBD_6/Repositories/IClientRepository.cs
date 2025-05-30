using APBD_6.Models;

namespace APBD_6.Repositories;

public interface IClientRepository
{
    Task<bool> ClientExistsAsync(int idClient);
    Task<bool> ClientHasTripsAsync(int idClient);
    Task DeleteClientAsync(int idClient);
    
    Task<bool> ClientWithPeselExistsAsync(string pesel);
    
    Task<Client_Trip?> GetClientTripAsync(int idTrip, string pesel);
    
    Task AddClientWithTripAsync(Client client, Client_Trip clientTrip);

}