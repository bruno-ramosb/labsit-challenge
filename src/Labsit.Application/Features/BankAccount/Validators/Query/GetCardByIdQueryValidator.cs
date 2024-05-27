using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.BankAccount.Commands.Query;

namespace Labsit.Application.Features.BankAccount.Validators.Query
{
    public class GetBankAccountIdQueryValidator : AbstractValidator<GetBankAccountByIdQuery>
    {
        public GetBankAccountIdQueryValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage(Messages.ID_REQUIRED);
        }
    }
}
