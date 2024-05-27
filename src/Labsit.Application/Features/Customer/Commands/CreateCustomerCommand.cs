using Labsit.Application.Common.Response;
using Labsit.Application.Features.Customer.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

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

        public CreateCustomerCommand()
        {
        }

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(11)]
        [DataType(DataType.Text)]
        public string Document { get; private set; }

        [Required]
        public DateOnly DateOfBirth { get; private set; }
    }
}
