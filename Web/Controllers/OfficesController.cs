using Application.Offices.CommandRequests;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class OfficesController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOffice([FromBody] CreateOfficeCommandRequest commandRequest)
    {
        var response = await Mediator.Send(commandRequest);

        return Ok(response);
    }
}