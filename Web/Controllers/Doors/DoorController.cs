using Application.Doors.CommandRequests;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Doors;

public class DoorController : ApiControllerBase
{        
    [HttpPut]
    public async Task<IActionResult> LockDoor([FromBody] LockDoorCommandRequest commandRequest)
    {
        await Mediator.Send(commandRequest);

        return NoContent();
    }
}