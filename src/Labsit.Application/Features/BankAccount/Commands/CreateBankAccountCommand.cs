using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Responses;
using MediatR;

namespace Labsit.Application.Features.BankAccount.Commands
{
    public class CreateBankAccountCommand : IRequest<Result<CreateBankAccountResponse>>
    {
        public CreateBankAccountCommand(int customerId)
        {
            CustomerId = customerId;
        }

        public int CustomerId { get; private set; }
    }
}
