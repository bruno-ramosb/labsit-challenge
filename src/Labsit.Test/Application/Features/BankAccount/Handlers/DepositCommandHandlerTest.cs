using Bogus;
using FluentAssertions;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Application.Features.BankAccount.Handlers;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Domain.Enums;
using Labsit.Infrastructure.Repositories;
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
            var depositAmount = 150m;

            var command = new Faker<DepositCommand>()
                .RuleFor(command => command.BankAccountId, 1)
                .RuleFor(command => command.Value, depositAmount)
                .RuleFor(command => command.TransactionType, ETransactionType.Debit)
                .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var handler = new DepositCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.Balance.Should().Be(depositAmount);
        }

        [Fact]
        public async Task Add_CreditValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var depositAmount = 330m;

            var command = new Faker<DepositCommand>()
                .RuleFor(command => command.BankAccountId, 1)
                .RuleFor(command => command.Value, depositAmount)
                .RuleFor(command => command.TransactionType, ETransactionType.Credit)
                .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var handler = new DepositCommandHandler(_validator, new BankAccountRepository(fixture.Context), unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.TotalCreditLimit.Should().Be(depositAmount);
            act.Data.AvailableCreditLimit.Should().Be(depositAmount);
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
            var act = await handler.Handle(new DepositCommand(), CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeFalse();
            act.Notifications.Should().Contain(Messages.BANK_ACCOUNT_ID_REQUIRED);
            act.Notifications.Should().Contain(Messages.VALUE_MUST_BE_GREATER_THAN_ZERO);
            act.Notifications.Should().Contain(Messages.TRANSCTION_TYPE_REQUIRED);
        }
    }
}
