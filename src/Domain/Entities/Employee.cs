using Domain.Common;

namespace Domain.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
