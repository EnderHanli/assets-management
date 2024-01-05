using Domain.Common;

namespace Domain.Entities
{
    public class Status : FullAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public int StatusTypeId { get; set; }
        public StatusType StatusType { get; set; } = null!;
        public string? Notes { get; set; }
        public bool IsSystem { get; set; }
        public List<Asset> Assets { get; set; } = new();
    }
}
