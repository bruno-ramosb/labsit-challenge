using FluentValidation;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Commands.Query;
using Labsit.Application.Features.BankAccount.Responses.Query;
using Labsit.Domain.Contracts.Repositories;
using MediatR;

namespace Labsit.Application.Features.BankAccount.Handlers.Query
{
    public class GetBankAccountByIdQueryHandler(IValidator<GetBankAccountByIdQuery> validator,
                                     IBankAccountRepository repository)
                                    : IRequestHandler<GetBankAccountByIdQuery, Result<BankAccountQueryModel>>
    {
        public async Task<Result<BankAccountQueryModel>> Handle(GetBankAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Result<BankAccountQueryModel>.Fail(validationResult.Errors);

            var result = await repository.GetByIdAsync(request.Id);
            if (result is null)
                return Result<BankAccountQueryModel>.Fail(Messages.BANK_ACCOUNT_NOT_FOUND);

            var model = new BankAccountQueryModel(result.Id,
                result.BranchCode,
                result.AccountNumber,
                result.Balance,
                result.TotalCreditLimit,
                result.AvailableCreditLimit,
                result.CustomerId);

            return Result<BankAccountQueryModel>.Successful(model);
        }
    }
}
