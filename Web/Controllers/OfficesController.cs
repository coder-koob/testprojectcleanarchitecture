using Application.Doors.CommandRequests;
using Application.Offices.CommandRequests;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Requests;

namespace Web.Controllers;

public class OfficesController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeCommandRequest commandRequest)
    {
        var response = await Mediator.Send(commandRequest);

        return Ok(response);
    }

    [HttpPost("{officeId}/door")]
    public async Task<IActionResult> AddDoor([FromRoute] Guid officeId, [FromBody] AddDoorRequest request)
    {
        await Mediator.Send(new AddDoorCommandRequest(officeId, request.Name));

        return NoContent();
    }

    [HttpPut("{officeId}/door/{doorId}/lock")]
    public async Task<IActionResult> LockDoor([FromRoute] Guid officeId, [FromRoute] Guid doorId)
    {
        await Mediator.Send(new LockDoorCommandRequest(officeId, doorId));

        return NoContent();
    }

    [HttpPut("{officeId}/door/{doorId}/unlock")]
    public async Task<IActionResult> UnlockDoor([FromRoute] Guid officeId, [FromRoute] Guid doorId)
    {
        await Mediator.Send(new UnlockDoorCommandRequest(officeId, doorId));

        return NoContent();
    }
}