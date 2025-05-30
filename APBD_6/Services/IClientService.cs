using APBD_6.DTOs;

namespace APBD_6.Services;

public interface IClientService
{
    Task<bool> DeleteClientAsync(int idClient);
    Task<string?> AssignClientToTripAsync(int idTrip, AssignClientToTripDto dto);
}