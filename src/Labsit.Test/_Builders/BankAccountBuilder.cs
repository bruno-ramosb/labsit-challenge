using Labsit.Application.Features.BankAccount.Commands;
using Labsit.Domain.Entities;
using Labsit.Domain.Enums;

namespace Labsit.Test._Builders
{
    public class BankAccountBuilder
    {
        private string _branchCode = "001";
        private string _accountNumber = "12345678";
        private decimal _balance = 0;
        private decimal _totalCreditLimit = 0;
        private decimal _availableCreditLimit = 0;
        private int _customerId = 1;
        private int _bankAccountId = 1;
        private decimal _value = 150;

        private int _bankAccountIdWithFunds = 1;
        private int _bankAccountIdWithoutFunds = 2;

        public BankAccountBuilder New() { return new BankAccountBuilder(); }    

        public BankAccountBuilder WithFunds()
        {
            _customerId = 1;
            _balance = 1500;
            _totalCreditLimit = 1500;
            _availableCreditLimit = 1500;
            _accountNumber = "87654321";
            _bankAccountId = _bankAccountIdWithFunds;
            return this;
        }

        public BankAccountBuilder AddCredit(decimal value)
        {
            _totalCreditLimit += value;
            _availableCreditLimit += value;
            return this;
        }

        public BankAccountBuilder AddBalance(decimal value)
        {
            _balance += value;
            return this;
        }
        public BankAccountBuilder WithdrawAvailableCredit(decimal value)
        {
            _availableCreditLimit -= value;
            return this;
        }

        public BankAccountBuilder WithdrawBalance(decimal value)
        {
            _balance -= value;
            return this;
        }

        public BankAccountBuilder WithoutFunds()
        {
            _customerId = 2;
            _accountNumber = "12345678";
            _bankAccountId = _bankAccountIdWithoutFunds;
            return this;
        }

        public WithdrawCommand BuildWithdrawCommand(ETransactionType transactionType)
        {
            return new WithdrawCommand(_bankAccountId, _value, transactionType);
        }

        public DepositCommand BuildDepositCommand(ETransactionType transactionType)
        {
            return new DepositCommand(_bankAccountId, _value, transactionType);
        }

        public BankAccount Build()
        {
            return new BankAccount(_customerId, _branchCode, _accountNumber,_balance,_totalCreditLimit,_availableCreditLimit);
        }
    }
}
