using FluentAssertions;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Handlers;
using Labsit.Application.Features.BankAccount.Validators;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Repositories;
using Labsit.Test._Builders;
using Labsit.Test.Fixtures;
using NSubstitute;

namespace Labsit.Test.Application.Features.BankAccount.Handlers
{
    public class DepositCommandHandlerTest(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
    {
        private readonly DepositCommandValidator _validator = new();

        [Fact]
        public async Task Add_DebitValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var bankAccount = new BankAccountBuilder().WithFunds().AddBalance(150).Build();
            var command = new BankAccountBuilder()
                .WithFunds()
                .AddBalance(150)
                .BuildDepositCommand(ETransactionType.Debit);

            var unitOfWork = new UnitOfWork(fixture.Context);
            var handler = new DepositCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.Balance.Should().Be(bankAccount.Balance);
        }

        [Fact]
        public async Task Add_CreditValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var bankAccount = new BankAccountBuilder().WithFunds().AddCredit(150).Build();
            var command = new BankAccountBuilder()
                .WithFunds()
                .AddCredit(150)
                .BuildDepositCommand(ETransactionType.Credit);

            var unitOfWork = new UnitOfWork(fixture.Context);
            var handler = new DepositCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.TotalCreditLimit.Should().Be(bankAccount.TotalCreditLimit);
            act.Data.AvailableCreditLimit.Should().Be(bankAccount.AvailableCreditLimit);
        }

        [Fact]
        public async Task Add_InvalidCommandShoulReturnFailResult()
        {
            // Arrange
            var handler = new DepositCommandHandler(
                _validator,
                Substitute.For<IBankAccountRepository>(),
                Substitute.For<IUnitOfWork>());

            // Act
            var act = await handler.Handle(new DepositCommand(default,default, default), CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeFalse();
            act.Notifications.Should().Contain(Messages.BANK_ACCOUNT_ID_REQUIRED);
            act.Notifications.Should().Contain(Messages.VALUE_MUST_BE_GREATER_THAN_ZERO);
            act.Notifications.Should().Contain(Messages.TRANSCTION_TYPE_REQUIRED);
        }
    }
}
