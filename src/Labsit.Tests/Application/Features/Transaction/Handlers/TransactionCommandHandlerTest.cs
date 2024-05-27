using Bogus;
using FluentAssertions;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Application.Features.Transaction.Command;
using Labsit.Application.Features.Transaction.Handlers;
using Labsit.Application.Features.Transaction.Validators;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Repositories;
using Labsit.Test._Builders;
using Labsit.Test.Fixtures;
using MediatR;
using Moq;

namespace Labsit.Test.Application.Features.Transaction.Handlers
{
    public class TransactionCommandHandlerTest(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
    {
        private readonly CreateTransactionCommandValidator _validator = new();
        private readonly Mock<IMediator> _mockMediator = new Mock<IMediator>();

        [Fact]
        public async Task Buy_DebitValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var price = 150m;
            var cardFaker = new CardBuilder().New().WithFunds().BuildCardDto(ETransactionType.Debit);
            var command = new Faker<CreateTransactionCommand>()
                    .CustomInstantiator(f => new CreateTransactionCommand(
                        f.Commerce.ProductName(),
                        price,
                        cardFaker
                    ))
                    .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var withdrawResponse = Result<WithdrawResponse>.Successful(new WithdrawResponse(2,1350,1500,1500));

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<WithdrawCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(withdrawResponse);

            var handler = new CreateTransactionCommandHandler(_validator, _mockMediator.Object, new CardRepository(fixture.Context));

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_SHOP);
        }

        [Fact]
        public async Task Buy_CreditValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var price = 150m;
            var cardFaker = new CardBuilder().New().WithFunds().BuildCardDto(ETransactionType.Credit);
            var command = new Faker<CreateTransactionCommand>()
                    .CustomInstantiator(f => new CreateTransactionCommand(
                        f.Commerce.ProductName(),
                        price,
                        cardFaker
                    ))
                    .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);


            var withdrawResponse = Result<WithdrawResponse>.Successful(new WithdrawResponse(2, 1500, 1350, 1350));

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<WithdrawCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(withdrawResponse);

            var handler = new CreateTransactionCommandHandler(_validator, _mockMediator.Object, new CardRepository(fixture.Context));


            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_SHOP);
        }
    }
}
