using FluentValidation;
using Labsit.Application.Common.Constants;

namespace Labsit.Application.Features.BankAccount.Commands
{
    public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand> 
    {
        public CreateBankAccountCommandValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage(Messages.CUSTOMER_ID_REQUIRED);
        }
    }
}
