using APBD_6.DTOs;
using APBD_6.Models;
using APBD_6.Repositories;

namespace APBD_6.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly ITripRepository _tripRepository;

    public ClientService(IClientRepository clientRepository, ITripRepository tripRepository)
    {
        _clientRepository = clientRepository;
        _tripRepository = tripRepository;
    }

    public async Task<string?> AssignClientToTripAsync(int idTrip, AssignClientToTripDto dto)
    {
        if (await _clientRepository.ClientWithPeselExistsAsync(dto.Pesel))
            return "A client with this PESEL already exists.";

        var existingTrip = await _tripRepository.GetTripByIdAsync(idTrip);
        if (existingTrip == null || existingTrip.DateFrom <= DateTime.UtcNow)
            return "Trip does not exist or has already started.";

        var alreadyAssigned = await _clientRepository.GetClientTripAsync(idTrip, dto.Pesel);
        if (alreadyAssigned != null)
            return "Client is already signed up for this trip.";

        var newClient = new Client
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        var newClientTrip = new Client_Trip
        {
            IdTrip = idTrip,
            RegisteredAt = DateTime.UtcNow,
            PaymentDate = dto.PaymentDate,
            IdClientNavigation = newClient
        };

        await _clientRepository.AddClientWithTripAsync(newClient, newClientTrip);
        return null;
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        if (!await _clientRepository.ClientExistsAsync(idClient))
            return false;

        if (await _clientRepository.ClientHasTripsAsync(idClient))
            return false;

        await _clientRepository.DeleteClientAsync(idClient);
        return true;
    }
    
    
}