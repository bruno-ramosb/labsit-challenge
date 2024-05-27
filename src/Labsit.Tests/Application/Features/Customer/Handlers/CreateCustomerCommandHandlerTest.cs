using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Labsit.Application.Common.Constants;
using Labsit.Application.Features.Customer.Commands;
using Labsit.Application.Features.Customer.Handlers;
using Labsit.Application.Features.Customer.Validators;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Infrastructure.Repositories;
using Labsit.Test._Builders;
using Labsit.Test.Fixtures;
using NSubstitute;

namespace Labsit.Test.Application.Features.Customer.Handlers
{
    public class CreateCustomerCommandHandlerTest(EfSqliteFixture fixture) : IClassFixture<EfSqliteFixture>
    {
        private readonly CreateCustomerCommandValidator _validator = new();
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(fixture.Context);

        [Fact]
        public async Task Add_ValidCommand_ShouldReturnSucessResult()
        {
            //Arrange
            var command = new Faker<CreateCustomerCommand>()
            .CustomInstantiator(faker => new CreateCustomerCommand(
            faker.Person.FullName,
            faker.Person.Cpf(),
            DateOnly.FromDateTime(DateTime.Today.AddYears(-18))))
            .Generate();

            var handler = new CreateCustomerCommandHandler(_validator,new CustomerRepository(fixture.Context),_unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeTrue();
            act.Message.Should().Be(Messages.SUCCESSUL_OPERATION);
            act.Data.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Add_DuplicatedDocument_ShouldReturnFailResult()
        {
            //Arrange
            var customer = new CustomerBuilder().Build();
            var command = new Faker<CreateCustomerCommand>()
                .CustomInstantiator(faker => new CreateCustomerCommand(
                    faker.Person.FullName,
                    customer.Document,
                    DateOnly.FromDateTime(DateTime.Today.AddYears(-18))))
                    .Generate();

            var unitOfWork = new UnitOfWork(fixture.Context);

            var handler = new CreateCustomerCommandHandler(_validator, new CustomerRepository(fixture.Context), unitOfWork);

            // Act
            await handler.Handle(command, CancellationToken.None);
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeFalse();
            act.Notifications.Should().Contain(Messages.DOCUMENT_ALREADY_IN_USE);
        }

        [Fact]
        public async Task Add_UnderAge_ShouldReturnFailResult()
        {
            //Arrange
            var birthOfDateUnderAge = DateOnly.FromDateTime(DateTime.Today.AddYears(-18).AddDays(1));

            var command = new Faker<CreateCustomerCommand>()
            .CustomInstantiator(faker => new CreateCustomerCommand(
                faker.Person.FullName,
                faker.Person.Cpf(),
                birthOfDateUnderAge))
                .Generate();

            var handler = new CreateCustomerCommandHandler(_validator, new CustomerRepository(fixture.Context), _unitOfWork);

            // Act
            var act = await handler.Handle(command, CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeFalse();
            act.Notifications.Should().Contain(Messages.CUSTOMER_BE_AT_LEAST_18_YEARS_OLD);
        }

        [Fact]
        public async Task Add_InvalidCommandShoulReturnFailResult()
        {
            // Arrange
            var handler = new CreateCustomerCommandHandler(
                _validator,
                Substitute.For<ICustomerRepository>(),
                Substitute.For<IUnitOfWork>());

            // Act
            var act = await handler.Handle(new CreateCustomerCommand(default,default,default), CancellationToken.None);

            // Assert
            act.Should().NotBeNull();
            act.Success.Should().BeFalse();
            act.Notifications.Should().NotBeNullOrEmpty().And.OnlyHaveUniqueItems();
        }

    }
}
