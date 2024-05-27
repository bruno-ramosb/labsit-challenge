using Labsit.Api.Extensions;
using Labsit.Application.Features.Customer.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Labsit.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required] CreateCustomerCommand command) =>
            (await mediator.Send(command)).ToActionResult();

        [HttpPost("completeRegister")]
        public async Task<IActionResult> QuickRegister([FromBody][Required] QuickCustomerRegistrationCommand command) =>
            (await mediator.Send(command)).ToActionResult();
    }
}
