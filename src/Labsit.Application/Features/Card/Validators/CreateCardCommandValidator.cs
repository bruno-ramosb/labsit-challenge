using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.Card.Command;

namespace Labsit.Application.Features.Card.Validators
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(x => x.BankAccountId)
                .GreaterThan(0)
                .WithMessage(Messages.BANK_ACCOUNT_ID_REQUIRED);
        }
    }
}
