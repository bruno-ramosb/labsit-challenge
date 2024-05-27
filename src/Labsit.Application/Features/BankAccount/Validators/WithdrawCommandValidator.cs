using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.BankAccount.Commands;

namespace Labsit.Application.Features.BankAccount.Validators
{
    public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator()
        {
            RuleFor(x => x.BankAccountId)
                .GreaterThan(0)
                .WithMessage(Messages.BANK_ACCOUNT_ID_REQUIRED);

            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage(Messages.VALUE_MUST_BE_GREATER_THAN_ZERO);

            RuleFor(x => x.TransactionType)
                .IsInEnum()
                .WithMessage(Messages.TRANSCTION_TYPE_REQUIRED);
        }
    }
}
