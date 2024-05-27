using Bogus;
using FluentAssertions;
using Labsit.Application.Common.Constants;
using Labsit.Application.Common.Response;
using Labsit.Application.Dtos;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Responses;
using Labsit.Application.Features.Transaction.Command;
using Labsit.Application.Features.Transaction.Handlers;
using Labsit.Application.Features.Transaction.Validators;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Repositories;
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

            var card = fixture.GetValidCards().FirstOrDefault(x=>x.BankAccountId == 2);

            var cardFaker = new Faker<CardDto>()
                .RuleFor(x => x.Number, card.Number)
                .RuleFor(x => x.HolderName, card.HolderName)
                .RuleFor(x => x.Brand, card.Brand)
                .RuleFor(x => x.ExpiryDate, card.ExpiryDate)
                .RuleFor(x => x.VerificationCode, card.VerificationCode)
                .RuleFor(x => x.TransactionType, ETransactionType.Debit)
                .Generate();

            var command = new Faker<CreateTransactionCommand>()
                .RuleFor(command => command.Description, faker => faker.Commerce.ProductName())
                .RuleFor(command => command.Price, price)
                .RuleFor(command => command.Card, cardFaker)
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

            var card = fixture.GetValidCards().FirstOrDefault(x => x.BankAccountId == 2);

            var cardFaker = new Faker<CardDto>()
                .RuleFor(x => x.Number, card.Number)
                .RuleFor(x => x.HolderName, card.HolderName)
                .RuleFor(x => x.Brand, card.Brand)
                .RuleFor(x => x.ExpiryDate, card.ExpiryDate)
                .RuleFor(x => x.VerificationCode, card.VerificationCode)
                .RuleFor(x => x.TransactionType, ETransactionType.Credit)
                .Generate();

            var command = new Faker<CreateTransactionCommand>()
                .RuleFor(command => command.Description, faker => faker.Commerce.ProductName())
                .RuleFor(command => command.Price, price)
                .RuleFor(command => command.Card, cardFaker)
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
