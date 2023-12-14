namespace Application.Common.Models
{
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static Error NullValue = new("error.NullValue", "Null value was provided");
    }
}
