using APBD_6.Models;

namespace APBD_6.Repositories;

public interface ITripRepository
{
    Task<List<Trip>> GetPagedTripsAsync(int skip, int take);
    Task<int> GetTotalTripsCountAsync();
    Task<Trip?> GetTripByIdAsync(int idTrip);
}