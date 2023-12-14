using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Status : FullAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public StatusType StatusType { get; set; }
        public string? Notes { get; set; }
    }
}
