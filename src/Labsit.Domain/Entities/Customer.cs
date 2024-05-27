using Labsit.Domain.Contracts.Entities;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }

        [Required]
        [MaxLength(11)]
        public string Document { get; private set; }

        [Required]
        public DateOnly DateOfBirth { get; private set; }

        public BankAccount BankAccount { get; private set; }
    }
}
