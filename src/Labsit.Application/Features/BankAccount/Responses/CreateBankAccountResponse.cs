using Labsit.Application.Common.Response;

namespace Labsit.Application.Features.BankAccount.Responses
{
    public class CreateBankAccountResponse(int id) : IResponse
    {
        public int Id { get; } = id;
    }
}
