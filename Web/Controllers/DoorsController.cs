using Application.Doors.CommandRequests;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class DoorsController : ApiControllerBase
{   
    [HttpPost]
    public async Task<IActionResult> AddDoor([FromBody] AddDoorCommandRequest commandRequest)
    {
        await Mediator.Send(commandRequest);

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> LockDoor([FromBody] LockDoorCommandRequest commandRequest)
    {
        await Mediator.Send(commandRequest);

        return NoContent();
    }
}