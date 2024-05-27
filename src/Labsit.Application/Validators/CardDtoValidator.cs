using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Dtos;

namespace Labsit.Application.Validators
{
    public class CardDtoValidator : AbstractValidator<CardDto>
    {
        public CardDtoValidator()
        {
            RuleFor(x => x.Number).NotEmpty().WithMessage(Messages.CARD_NUMBER_REQUIRED);
            RuleFor(x => x.HolderName).NotEmpty().WithMessage(Messages.HOLDER_NAME_REQUIRED);
            RuleFor(x => x.Brand).IsInEnum().WithMessage(Messages.BRAND_REQUIRED);
            RuleFor(x => x.TransactionType).IsInEnum().WithMessage(Messages.TRANSCTION_TYPE_REQUIRED);

            RuleFor(x => x.ExpiryDate)
                .Must(date => date >= DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage(Messages.EXPIRED_CARD);

            RuleFor(x => x.VerificationCode)
                .NotEmpty()
                .WithMessage(Messages.VERIFICATION_CODE_REQUIRED)
                .Must(code => code.Length == 3)
                .WithMessage(Messages.VERIFICATION_CODE_LENGHT_MUST_BE_EQUALS_3);
        }
    }
}
