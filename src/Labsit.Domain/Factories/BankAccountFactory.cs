using Labsit.Domain.Entities;

namespace Labsit.Domain.Factories
{
    public class BankAccountFactory
    {
        public static BankAccount Create(int customerId, string branchCode, string accountNumber) => new(customerId, branchCode, accountNumber);
    }
}
