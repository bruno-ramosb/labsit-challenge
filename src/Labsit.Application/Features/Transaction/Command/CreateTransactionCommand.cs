using Labsit.Application.Common.Response;
using Labsit.Application.Dtos;
using Labsit.Application.Features.Transaction.Responses;
using MediatR;

namespace Labsit.Application.Features.Transaction.Command
{
    public class CreateTransactionCommand : IRequest<Result<CreateTransactionResponse>>
    {
        public CreateTransactionCommand() { }
        public CreateTransactionCommand(string description, decimal price, CardDto card)
        {
            Description = description;
            Price = price;
            Card = card;
        }

        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public CardDto Card { get; private set; }
    }
}
