using Domain.Common;

namespace Domain.Entities
{
    public class Department : Entity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}
