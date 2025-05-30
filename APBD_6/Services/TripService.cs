using APBD_6.DTOs;
using APBD_6.Repositories;
using APBD_6.Services;

namespace APBD_6.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<TripListResponseDto> GetTripsAsync(int pageNum, int pageSize)
    {
        int totalTrips = await _tripRepository.GetTotalTripsCountAsync();
        int allPages = (int)Math.Ceiling((double)totalTrips / pageSize);

        var trips = await _tripRepository.GetPagedTripsAsync((pageNum - 1) * pageSize, pageSize);

        var tripDtos = trips.Select(t => new TripDto
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            Countries = t.IdCountries.Select(ct => new CountryDto
            {
                Name = ct.Name
            }).ToList(),
            Clients = t.Client_Trips.Select(ct => new ClientDto
            {
                FirstName = ct.IdClientNavigation.FirstName,
                LastName = ct.IdClientNavigation.LastName
            }).ToList()
        }).ToList();

        return new TripListResponseDto
        {
            PageNum = pageNum,
            PageSize = pageSize,
            AllPages = allPages,
            Trips = tripDtos
        };
    }


}