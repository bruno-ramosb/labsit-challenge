using Labsit.Domain.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Labsit.Domain.Entities
{
    [Table("Customer", Schema = "Finance")]
    public class Customer : AuditableEntity<int>
    {
        public Customer(string name, string document, DateOnly dateOfBirth)
        {
            Name = name;
            Document = document;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; set; }
        public string Document { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public BankAccount BankAccount { get; set; }
    }
}
