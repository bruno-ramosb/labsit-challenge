using Labsit.Application.Common.Response;

namespace Labsit.Application.Features.Customer.Responses
{
    public class QuickCustomerRegistrationResponse(int id, int bankAccountId, int cardId) : IResponse
    {
        public int Id { get; } = id;
        public int BankAccountId { get; } = bankAccountId;
        public int CardId { get; } = cardId;
    }
}
