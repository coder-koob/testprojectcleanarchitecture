using Application.Doors.CommandRequests;
using Application.Offices.CommandRequests;
using Application.Offices.Queries;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Requests;

namespace Web.Controllers;

public class OfficesController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeCommandRequest commandRequest, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(commandRequest, cancellationToken);

        return CreatedAtAction(nameof(GetOffice), new { officeId = response.OfficeId }, response);
    }

    [HttpGet("{officeId}")]
    public async Task<IActionResult> GetOffice([FromRoute] Guid officeId, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetOfficeQuery(officeId), cancellationToken);

        return Ok(response);
    }

    [HttpPost("{officeId}/door")]
    public async Task<IActionResult> AddDoor([FromRoute] Guid officeId, [FromBody] AddDoorRequest request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new AddDoorCommandRequest(officeId, request.Name), cancellationToken);

        return CreatedAtAction(nameof(GetOffice), new { officeId = response.OfficeId }, response);
    }

    [HttpPut("{officeId}/door/{doorId}/lock")]
    public async Task<IActionResult> LockDoor([FromRoute] Guid officeId, [FromRoute] Guid doorId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new LockDoorCommandRequest(officeId, doorId), cancellationToken);

        return NoContent();
    }

    [HttpPut("{officeId}/door/{doorId}/unlock")]
    public async Task<IActionResult> UnlockDoor([FromRoute] Guid officeId, [FromRoute] Guid doorId, CancellationToken cancellationToken)
    {
        await Mediator.Send(new UnlockDoorCommandRequest(officeId, doorId), cancellationToken);

        return NoContent();
    }
}