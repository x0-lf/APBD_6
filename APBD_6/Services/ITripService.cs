using APBD_6.DTOs;

namespace APBD_6.Services;

public interface ITripService
{
    Task<TripListResponseDto> GetTripsAsync(int pageNum, int pageSize);
}