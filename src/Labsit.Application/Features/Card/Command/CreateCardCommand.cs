using Labsit.Application.Common.Response;
using Labsit.Application.Features.Card.Responses;
using Labsit.Domain.Enums;
using MediatR;

namespace Labsit.Application.Features.Card.Command
{
    public class CreateCardCommand : IRequest<Result<CreateCardResponse>>
    {
        public CreateCardCommand(int bankAccountId, ECardBrand? brand)
        {
            BankAccountId = bankAccountId;
            Brand = brand;
        }

        public int BankAccountId { get; private set; }  
        public ECardBrand? Brand { get; private set; }
    }
}
