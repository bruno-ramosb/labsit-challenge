using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Extensions;
using Labsit.Application.Features.Customer.Commands;

namespace Labsit.Application.Features.Customer.Validators
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage(Messages.NAME_REQUIRED);


            RuleFor(command => command.Document)
                .Must(x => x.IsCpf())
                .WithMessage(Messages.DOCUMENT_INVALID);


            RuleFor(command => command.DateOfBirth)
                .Must(IsAtLeast18YearsOld)
                .WithMessage(Messages.CUSTOMER_BE_AT_LEAST_18_YEARS_OLD);
        }

        public static bool IsAtLeast18YearsOld(DateOnly dataOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dataOfBirth.Year;

            if (dataOfBirth.ToDateTime(TimeOnly.MinValue).Date > today.AddYears(-age)) age--;

            return age >= 18;
        }
    }
}
