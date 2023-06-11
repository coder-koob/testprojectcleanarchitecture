using Application.Doors.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class DoorsController : ApiControllerBase
{
    [HttpGet("{doorId}/history")]
    public async Task<IActionResult> GetOffice([FromRoute] Guid doorId, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetDoorHistoryQuery(doorId), cancellationToken);

        return Ok(response);
    }
}