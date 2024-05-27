using Labsit.Domain.Entities;

namespace Labsit.Domain.Factories
{
    public class CustomerFactory
    {
        public static Customer Create(string name, string document, DateOnly dateOfBirth) => new(name, document, dateOfBirth);
    }
}
