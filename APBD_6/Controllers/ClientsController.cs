using APBD_6.DTOs;
using APBD_6.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpPost("/api/trips/{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] AssignClientToTripDto dto)
    {
        var error = await _clientService.AssignClientToTripAsync(idTrip, dto);
        if (error != null)
            return BadRequest(error);

        return Ok("Client assigned successfully.");
    }

    
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        
        var result = await _clientService.DeleteClientAsync(idClient);

        if (!result)
            return BadRequest("Client cannot be deleted: either does not exist or has trips assigned.");

        return NoContent();
    }
}