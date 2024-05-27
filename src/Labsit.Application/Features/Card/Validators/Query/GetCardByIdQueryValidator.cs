using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.Card.Command.Query;

namespace Labsit.Application.Features.Card.Validators.Query
{
    public class GetCardByIdQueryValidator : AbstractValidator<GetCardByIdQuery>
    {
        public GetCardByIdQueryValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage(Messages.ID_REQUIRED);
        }
    }
}
