using Labsit.Application.Dtos;
using Labsit.Application.Features.Customer.Commands;
using Labsit.Domain.Entities;
using Labsit.Domain.Enums;

namespace Labsit.Test._Builders
{
    public class CardBuilder
    {
        private string _number = "5192858385719792";
        private string _holderName= "Roberto S A";
        private ECardBrand _brand= ECardBrand.Visa;
        private DateOnly _expiryDate = DateOnly.Parse("2040-01-01");
        private string _verificationCode = "123";
        private int _bankAccountId = 1;

        public CardBuilder New() { return new CardBuilder(); }

        public CardBuilder WithFunds()
        {
            _bankAccountId = 1;
            _number = "5192858385719792";
            _holderName = "Roberto S A";
            _brand = ECardBrand.Visa;
            return this;
        }

        public CardBuilder WithoutFunds()
        {
            _bankAccountId = 2;
            _number = "5210680963269075";
            _holderName = "Maria R A";
            _brand = ECardBrand.MasterCard;
            return this;
        }

        public Card BuildCard()
        {
            return new Card(_bankAccountId, _number, _holderName, _verificationCode, _brand, _expiryDate);
        }

        public CardDto BuildCardDto(ETransactionType transactionType)
        {
            return new CardDto(_number, _holderName, _brand, _expiryDate, _verificationCode, transactionType);
        }
    }
    public class CustomerBuilder
    {
        private string _name = "Roberto Santos Andrade";
        private string _document = "57208611068";
        private DateOnly _dateOfBirth = DateOnly.FromDateTime(DateTime.Today.AddYears(-18));

        public CustomerBuilder New() { return new CustomerBuilder(); }

        public CustomerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CustomerBuilder WithDocument(string document)
        {
            _document = document;
            return this;
        }

        public CustomerBuilder WithDateOfBirth(DateOnly dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
            return this;
        }

        public Customer Build()
        {
            return new Customer(_name, _document, _dateOfBirth);
        }

        public CreateCustomerCommand BuildCreateCustomerCommand()
        {
            return new CreateCustomerCommand(_name, _document, _dateOfBirth);
        }
    }
}
