using Labsit.Application.Common.Response;
using Labsit.Application.Features.Card.Responses.Query;
using MediatR;

namespace Labsit.Application.Features.Card.Command.Query
{
    public class GetCardByIdQuery(int id) : IRequest<Result<CardQueryModel>>
    {
        public int Id { get; } = id;
    }
}
