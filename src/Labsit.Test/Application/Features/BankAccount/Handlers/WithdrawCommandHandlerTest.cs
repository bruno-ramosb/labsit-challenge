using Bogus;
using FluentAssertions;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Handlers;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Repositories;
using Labsit.Test.Fixtures;

namespace Labsit.Test.Application.Features.BankAccount.Handlers
{
    public class WithdrawCommandHandlerTest(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
    {
        private readonly WithdrawCommandValidator _validator = new();

        [Fact]
        public async Task Withdraw_DebitValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var withdrawAmount = 150m;

            var command = new Faker<WithdrawCommand>()
                .RuleFor(command => command.BankAccountId, 2)
                .RuleFor(command => command.Value, withdrawAmount)
                .RuleFor(command => command.TransactionType, ETransactionType.Debit)
                .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var handler = new WithdrawCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.Balance.Should().Be(1350);
        }

        [Fact]
        public async Task Withdraw_CreditValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var withdrawAmount = 150m;

            var command = new Faker<WithdrawCommand>()
                .RuleFor(command => command.BankAccountId, 2)
                .RuleFor(command => command.Value, withdrawAmount)
                .RuleFor(command => command.TransactionType, ETransactionType.Credit)
                .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var handler = new WithdrawCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.AvailableCreditLimit.Should().Be(1350);
        }

        [Fact]
        public async Task Withdraw_Debit_Greater_Than_Funds_ShouldReturnFailResult()
        {
            //Arrange
            var withdrawAmount = 15001;

            var command = new Faker<WithdrawCommand>()
                .RuleFor(command => command.BankAccountId, 2)
                .RuleFor(command => command.Value, withdrawAmount)
                .RuleFor(command => command.TransactionType, ETransactionType.Debit)
                .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var handler = new WithdrawCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeFalse();
            act.Notifications.Should().Contain(Messages.INSUFFICIENT_FUNDS);
        }

        [Fact]
        public async Task Withdraw_Credit_Greater_Than_Funds_ShouldReturnFailResult()
        {
            //Arrange
            var withdrawAmount = 15001;

            var command = new Faker<WithdrawCommand>()
                .RuleFor(command => command.BankAccountId, 2)
                .RuleFor(command => command.Value, withdrawAmount)
                .RuleFor(command => command.TransactionType, ETransactionType.Credit)
                .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var handler = new WithdrawCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeFalse();
            act.Notifications.Should().Contain(Messages.INSUFFICIENT_FUNDS);
        }
    }
}
