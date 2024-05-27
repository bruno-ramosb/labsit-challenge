using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.Customer.Commands;

namespace Labsit.Application.Features.Customer.Validators
{
    public class QuickCustomerRegistrationCommandValidator : AbstractValidator<QuickCustomerRegistrationCommand>
    {
        public QuickCustomerRegistrationCommandValidator()
        {
            RuleFor(x => x.Brand)
                .IsInEnum()
                .WithMessage(Messages.BRAND_REQUIRED);
        }
    }
}
