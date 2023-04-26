using ChocolateManagementSystem.Application.Features.ChocolateBars.CreateChocolateBar;
using ChocolateManagementSystem.Application.Features.ChocolateBars.DeleteChocolateBar;
using ChocolateManagementSystem.Application.Features.ChocolateBars.GetAllChocolateBarsByFactory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChocolateBarsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChocolateBarsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var response = await _mediator.Send(new GetChocolateBarsByFactoryQuery());
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChocolateBar(CreateChocolateBarCommand payload)
    {
        var newlyCreatedChocolateId = await _mediator.Send(payload);
        return Ok(newlyCreatedChocolateId);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteChocolateBar(DeleteChocolateBarCommand payload)
    {
        await _mediator.Send(payload);
        return NoContent();
    }
}
