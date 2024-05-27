using FluentAssertions;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.BankAccount.Handlers;
using Labsit.Application.Features.BankAccount.Validators;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Repositories;
using Labsit.Test._Builders;
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
            var bankAccount = new BankAccountBuilder().New()
                .WithFunds()
                .WithdrawBalance(150)
                .Build();

            var command = new BankAccountBuilder()
                .New()
                .WithFunds()
                .BuildWithdrawCommand(ETransactionType.Debit);

            var unitOfWork = new UnitOfWork(fixture.Context);
            var handler = new WithdrawCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.Balance.Should().Be(bankAccount.Balance);
        }

        [Fact]
        public async Task Withdraw_CreditValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var bankAccount = new BankAccountBuilder().New()
                .WithFunds()
                .WithdrawAvailableCredit(150)
                .Build();
            var command = new BankAccountBuilder()
                .New()
                .WithFunds()
                .BuildWithdrawCommand(ETransactionType.Credit);

            var unitOfWork = new UnitOfWork(fixture.Context);
            var handler = new WithdrawCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.AvailableCreditLimit.Should().Be(bankAccount.AvailableCreditLimit);
        }

        [Fact]
        public async Task Withdraw_Debit_Greater_Than_Funds_ShouldReturnFailResult()
        {
            //Arrange
            var command = new BankAccountBuilder().New()
                .WithoutFunds()
                .BuildWithdrawCommand(ETransactionType.Debit);

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
            var command = new BankAccountBuilder().New()
                .WithoutFunds()
                .BuildWithdrawCommand(ETransactionType.Credit);

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
