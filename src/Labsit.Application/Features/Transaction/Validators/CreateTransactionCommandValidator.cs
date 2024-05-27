using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.Transaction.Command;
using Labsit.Application.Validators;

namespace Labsit.Application.Features.Transaction.Validators
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(Messages.DESCRIPTION_REQUIRED);
            RuleFor(x => x.Price).GreaterThan(0).WithMessage(Messages.VALUE_MUST_BE_GREATER_THAN_ZERO);

            RuleFor(x => x.Card).SetValidator(new CardDtoValidator());
        }
    }
}
