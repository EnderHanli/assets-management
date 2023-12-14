using Domain.Common;

namespace Domain.Entities
{
    public class Manufacturer : FullAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Url { get; set; }
        public string? SupportUrl { get; set; }
        public string? SupportPhoneNumber { get; set; }
        public string? SupportEmailAddress { get; set; }
        public string? Notes { get; set; }
    }
}
