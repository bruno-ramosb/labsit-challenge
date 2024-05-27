using Labsit.Api.Helper;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Commands.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Labsit.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BankAccountController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required] CreateBankAccountCommand command) =>
                (await mediator.Send(command)).ToActionResult();

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([Required] int id) =>
            (await mediator.Send(new GetBankAccountByIdQuery(id))).ToActionResult();

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody][Required] DepositCommand command) =>
                (await mediator.Send(command)).ToActionResult();

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody][Required] WithdrawCommand command) =>
                (await mediator.Send(command)).ToActionResult();
    }
}
