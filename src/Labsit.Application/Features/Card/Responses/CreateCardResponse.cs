using Labsit.Application.Common.Response;

namespace Labsit.Application.Features.Card.Responses
{
    public class CreateCardResponse(int id) : IResponse
    {
        public int Id { get; } = id;
    }
}
