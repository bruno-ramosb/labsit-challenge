using Labsit.Application.Common.Response;
using Labsit.Application.Features.Customer.Responses;
using MediatR;

namespace Labsit.Application.Features.Customer.Commands
{
    public class CreateCustomerCommand : IRequest<Result<CreateCustomerResponse>>
    {
        public CreateCustomerCommand(string name, string document, DateOnly dateOfBirth)
        {
            Name = name;
            Document = document;
            DateOfBirth = dateOfBirth;
        }


        public string Name { get; private set; }

        public string Document { get; private set; }

        public DateOnly DateOfBirth { get; private set; }
    }
}
