using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Extensions;
using Labsit.Application.Features.Card.Command;
using Labsit.Application.Features.Card.Responses;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Enums;
using MediatR;

namespace Labsit.Application.Features.Card.Handlers
{
    public class CreateCardCommandHandler(IValidator<CreateCardCommand> validator,
                                          IBankAccountRepository repository,
                                          ICustomerRepository customerRepository,
                                          ICardRepository cardRepository,
                                          IUnitOfWork unitOfWork) : IRequestHandler<CreateCardCommand, Result<CreateCardResponse>>
    {

        private const ECardBrand DEFAULT_BRAND = ECardBrand.Visa;

        public async Task<Result<CreateCardResponse>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result<CreateCardResponse>.Fail(validationResult.Errors);

            var bankAccount = await repository.GetByIdAsync(request.BankAccountId);
            if (bankAccount is null)
                return Result<CreateCardResponse>.Fail(Messages.BANK_ACCOUNT_NOT_FOUND);

            if(await cardRepository.HasCardInBankAccount(bankAccount.Id))
                return Result<CreateCardResponse>.Fail(Messages.CARD_ALREADY_CREATED);

            var customer = await customerRepository.GetByIdAsync(bankAccount.CustomerId);

            var card = await CreateCard(customer, request, cancellationToken);

            var response = new CreateCardResponse(card.Id);
            
            return Result<CreateCardResponse>.Successful(response);
        }

        private async Task<Domain.Entities.Card> CreateCard(Domain.Entities.Customer customer,CreateCardCommand request, CancellationToken cancellationToken)
        {
            var cardNumber = CardExtensions.GenerateCreditCardNumber();
            var holderName = CardExtensions.GenerateHolderName(customer.Name);
            var expiryDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(4));
            var brand = request.Brand.HasValue ? request.Brand.Value : DEFAULT_BRAND;
            var verificationCode = new Random().Next(100, 999).ToString();

            var card = new Domain.Entities.Card(request.BankAccountId, cardNumber, holderName, verificationCode, brand, expiryDate);
            await cardRepository.Add(card);

            await unitOfWork.Commit(cancellationToken);
            return card;
        }
    }
}
