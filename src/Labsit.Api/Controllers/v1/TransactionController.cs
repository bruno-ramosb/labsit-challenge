using Labsit.Api.Extensions;
using Labsit.Application.Features.Transaction.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Labsit.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required] CreateTransactionCommand command) =>
            (await mediator.Send(command)).ToActionResult();
    }
}
