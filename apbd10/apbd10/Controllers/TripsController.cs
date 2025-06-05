using apbd10.Data;
using apbd10.Exceptions;
using apbd10.Models;
using apbd10.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd10.Controllers;

[ApiController]
[Route("[controller]")]
public class TripsController(IDbService service): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTrips()
    {
        return Ok( await service.GetTrips());
    }

    [HttpPut]
    [Route("/{tripId}/clients")]
    public async Task<IActionResult> SignClientToTrip([FromRoute] int tripId,[FromBody] Client client)
    {
        try
        {
            await service.SignClient(tripId,client);
            return NoContent();
        }
        catch (NotFoundException e)
        {
          return NotFound(e.Message);  
        }
    }
    
    
    
    
}