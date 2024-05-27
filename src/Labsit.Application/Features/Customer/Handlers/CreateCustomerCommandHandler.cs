using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Extensions;
using Labsit.Application.Features.Customer.Commands;
using Labsit.Application.Features.Customer.Responses;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Factories;
using MediatR;

namespace Labsit.Application.Features.Customer.Handlers
{
    public class CreateCustomerCommandHandler(IValidator<CreateCustomerCommand> validator,
                                              ICustomerRepository repository,
                                              IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Result<CreateCustomerResponse>>
    {

        public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result<CreateCustomerResponse>.Fail(validationResult.Errors);

            var documentNumber = request.Document.GetOnlyNumbers();

            if (await repository.IsDocumentInUse(documentNumber))
                return Result<CreateCustomerResponse>.Fail(Messages.DOCUMENT_ALREADY_IN_USE);

            var customer = CustomerFactory.Create(request.Name, documentNumber, request.DateOfBirth);

            await repository.Add(customer);

            await unitOfWork.Commit(cancellationToken);

            var response = new CreateCustomerResponse(customer.Id);

            return Result<CreateCustomerResponse>.Successful(response);
        }
    }
}
