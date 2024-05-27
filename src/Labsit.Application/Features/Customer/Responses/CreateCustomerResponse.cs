using Labsit.Application.Common.Response;

namespace Labsit.Application.Features.Customer.Responses
{
    public class CreateCustomerResponse(int id) : IResponse
    {
        public int Id { get; } = id;
    }
}
