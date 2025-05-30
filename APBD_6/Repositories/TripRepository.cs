using APBD_6.Data;
using APBD_6.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_6.Repositories;

public class TripRepository : ITripRepository
{
    private readonly AppDbContext _context;

    public TripRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Trip>> GetPagedTripsAsync(int skip, int take)
    {
        return await _context.Trips
            .AsNoTracking()
            .Include(t => t.Client_Trips)
            .ThenInclude(ct => ct.IdClientNavigation)
            .Include(t => t.IdCountries)
            .OrderByDescending(t => t.DateFrom)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }
    
    public async Task<int> GetTotalTripsCountAsync()
    {
        return await _context.Trips
            .CountAsync();
    }
    
    public async Task<Trip?> GetTripByIdAsync(int idTrip)
    {
        return await _context.Trips
            .FirstOrDefaultAsync(t => t.IdTrip == idTrip);
    }
}