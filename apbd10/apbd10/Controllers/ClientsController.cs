using apbd10.Exceptions;
using apbd10.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd10.Controllers;
[ApiController]
[Route("[controller]")]
public class ClientsController(IDbService service):ControllerBase
{
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        try
        {
            await service.RemoveClient(id);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}