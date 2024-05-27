using Labsit.Application.Common.Response;
using Labsit.Application.Features.Customer.Responses;
using Labsit.Domain.Enums;
using MediatR;

namespace Labsit.Application.Features.Customer.Commands
{
    public class QuickCustomerRegistrationCommand : IRequest<Result<QuickCustomerRegistrationResponse>>
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ECardBrand? Brand { get; set; }
    }
}
