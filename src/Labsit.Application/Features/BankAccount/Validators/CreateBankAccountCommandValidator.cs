using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.BankAccount.Commands;

namespace Labsit.Application.Features.BankAccount.Validators
{
    public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
    {
        public CreateBankAccountCommandValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage(Messages.CUSTOMER_ID_REQUIRED);
        }
    }
}
