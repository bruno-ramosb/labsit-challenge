using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Application.Features.Transaction.Command;
using Labsit.Application.Features.Transaction.Responses;
using Labsit.Domain.Contracts.Repositories;
using MediatR;

namespace Labsit.Application.Features.Transaction.Handlers
{
    public class CreateTransactionCommandHandler(IValidator<CreateTransactionCommand> validator,
                                                 IMediator mediator,
                                                 ICardRepository cardRepository) : IRequestHandler<CreateTransactionCommand, Result<CreateTransactionResponse>>
    {
        public async Task<Result<CreateTransactionResponse>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var validCard = await cardRepository.GetValidCard(request.Card.Number, 
                request.Card.HolderName, 
                request.Card.VerificationCode, 
                request.Card.Brand, 
                request.Card.ExpiryDate);

            if (validCard is null)
                return Result<CreateTransactionResponse>.Fail(Messages.INVALID_CARD_DETAILS);

            var withdrawResponse = await Withdraw(validCard.BankAccountId, request, cancellationToken);
            if (!withdrawResponse.Success)
                return Result<CreateTransactionResponse>.Fail(withdrawResponse.Notifications);

            return Result<CreateTransactionResponse>.Successful(new (),Messages.SUCCESSUL_SHOP);
        }

        private async Task<Result<WithdrawResponse>> Withdraw(int bankAccountId,CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var withdrawCommand = new WithdrawCommand(bankAccountId, request.Price, request.Card.TransactionType);
            return await mediator.Send(withdrawCommand, cancellationToken);
        }
    }
}
