using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Application.Features.Card.Command;
using Labsit.Application.Features.Card.Responses;
using Labsit.Application.Features.Customer.Commands;
using Labsit.Application.Features.Customer.Responses;
using Labsit.Domain.Enums;
using MediatR;

namespace Labsit.Application.Features.Customer.Handlers
{
    public class QuickCustomerRegistrationCommandHandler(IMediator mediator) : IRequestHandler<QuickCustomerRegistrationCommand, Result<QuickCustomerRegistrationResponse>>
    {
        public async Task<Result<QuickCustomerRegistrationResponse>> Handle(QuickCustomerRegistrationCommand request, CancellationToken cancellationToken)
        {
            var customerResponse = await CreateCustomer(request, cancellationToken);
            if (!customerResponse.Success)
                return Result<QuickCustomerRegistrationResponse>.Fail(customerResponse.Notifications, customerResponse.StatusCode);

            var bankAccountResponse = await CreateBankAccount(customerResponse.Data.Id, cancellationToken); 
            if (!bankAccountResponse.Success)
                return Result<QuickCustomerRegistrationResponse>.Fail(bankAccountResponse.Notifications, bankAccountResponse.StatusCode);

            var cardResponse = await CreateCard(bankAccountResponse.Data.Id,request.Brand, cancellationToken);
            if (!bankAccountResponse.Success)
                return Result<QuickCustomerRegistrationResponse>.Fail(cardResponse.Notifications, cardResponse.StatusCode);


            var response = new QuickCustomerRegistrationResponse(customerResponse.Data.Id, bankAccountResponse.Data.Id, cardResponse.Data.Id);
            return Result<QuickCustomerRegistrationResponse>.Successful(response);
        }

        private async Task<Result<CreateCustomerResponse>> CreateCustomer(QuickCustomerRegistrationCommand request, CancellationToken cancellationToken)
        {
            var createCustomerCommand = new CreateCustomerCommand(request.Name, request.Document, request.DateOfBirth);
            return await mediator.Send(createCustomerCommand, cancellationToken);
        }

        private async Task<Result<CreateBankAccountResponse>> CreateBankAccount(int customerId, CancellationToken cancellationToken)
        {
            var createBankAccountCommand = new CreateBankAccountCommand(customerId);
            return await mediator.Send(createBankAccountCommand, cancellationToken);
        }

        private async Task<Result<CreateCardResponse>> CreateCard(int bankAccountId,ECardBrand? brand, CancellationToken cancellationToken)
        {
            var createBankAccountCommand = new CreateCardCommand(bankAccountId, brand);
            return await mediator.Send(createBankAccountCommand, cancellationToken);
        }
    }
}
