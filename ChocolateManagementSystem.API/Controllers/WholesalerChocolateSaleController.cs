using ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.CreateChocolateStock;
using ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.RequestChocolateQuote;
using ChocolateManagementSystem.Application.Features.WholesalerChocolateSale.UpdateChocolateStock;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerChocolateSaleController : ControllerBase
    {

        private readonly IMediator _mediator;

        public WholesalerChocolateSaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddChocolateSale(CreateChocolateStockCommand payload)
        {
            var newlyCreatedStock = await _mediator.Send(payload);
            return Ok(newlyCreatedStock);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateChocolateSale(UpdateChocolateStockCommand payload)
        {
            await _mediator.Send(payload);
            return NoContent();
        }

        [HttpPost("RequestQuote")]
        public async Task<IActionResult> RequestQuote(RequestChocolateQuoteQuery payload)
        {
            var requestFeedback = await _mediator.Send(payload);
            return Ok(requestFeedback);
        }
    }
}
