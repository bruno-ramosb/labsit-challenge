using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Responses.Query;
using MediatR;

namespace Labsit.Application.Features.BankAccount.Commands.Query
{
    public class GetBankAccountByIdQuery(int id) : IRequest<Result<BankAccountQueryModel>>
    {
        public int Id { get; } = id;
    }
}
