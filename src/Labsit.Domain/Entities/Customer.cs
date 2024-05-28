using Labsit.Domain.Contracts.Entities;

namespace Labsit.Domain.Entities
{
    public class Customer : AuditableEntity<int>
    {
        public Customer(string name, string document, DateOnly dateOfBirth)
        {
            Name = name;
            Document = document;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; private set; }
        public string Document { get; private set; }
        public DateOnly DateOfBirth { get; private set; }
        public BankAccount BankAccount { get; private set; }
    }
}
