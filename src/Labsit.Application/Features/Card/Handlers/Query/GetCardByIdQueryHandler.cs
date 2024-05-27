using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Features.Card.Command.Query;
using Labsit.Application.Features.Card.Responses.Query;
using Labsit.Domain.Contracts.Repositories;
using MediatR;

namespace Labsit.Application.Features.Card.Handlers.Query
{
    public class GetCardByIdQueryHandler(IValidator<GetCardByIdQuery> validator,
                                         ICardRepository cardRepository) 
                                        : IRequestHandler<GetCardByIdQuery, Result<CardQueryModel>>
    {
        public async Task<Result<CardQueryModel>> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result<CardQueryModel>.Fail(validationResult.Errors);

            var result = await cardRepository.GetByIdAsync(request.Id);
            if (result is null)
                return Result<CardQueryModel>.Fail(Messages.CARD_NOT_FOUND);

            var model = new CardQueryModel(result.Id, 
                result.Number, 
                result.HolderName, 
                result.Brand, 
                result.ExpiryDate, 
                result.VerificationCode,
                result.BankAccountId);

            return Result<CardQueryModel>.Successful(model);
        }
    }
}
