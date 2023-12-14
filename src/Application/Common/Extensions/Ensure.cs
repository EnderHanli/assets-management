using System.Runtime.CompilerServices;

namespace Application.Common.Extensions
{
    public static class Ensure
    {
        public static void NotNullOrWhiteSpace(
            string? value,
            string? message = null,
            [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(message ?? "The value can't be null", paramName);
            }
        }
    }
}
