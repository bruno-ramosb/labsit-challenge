using Labsit.Api.Extensions;
using Labsit.Application.Features.Card.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Labsit.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CardController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required] CreateCardCommand command) =>
            (await mediator.Send(command)).ToActionResult();
    }
}
