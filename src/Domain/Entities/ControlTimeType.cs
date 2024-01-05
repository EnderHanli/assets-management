using Domain.Common;

namespace Domain.Entities
{
    public class ControlTimeType : Enumeration
    {
        public static readonly ControlTimeType OneMonth = new(1, "1 month from now");
        public static readonly ControlTimeType ThreeMonths = new(2, "3 months from now");
        public static readonly ControlTimeType SixMonths = new(3, "6 months from now");
        public static readonly ControlTimeType TwelveMonths = new(4, "12 months from now");
        public static readonly ControlTimeType TwentyFourMonths = new(5, "24 months from now");
        public static readonly ControlTimeType ThirtySixMonths = new(6, "36 months from now");

        private ControlTimeType(int id, string name)
        : base(id, name)
        {
        }

        public static IEnumerable<ControlTimeType> List() =>
            new[] { OneMonth, ThreeMonths, SixMonths, TwelveMonths, TwentyFourMonths, ThirtySixMonths };

        public static ControlTimeType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for ControlTimeType: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static ControlTimeType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for ControlTimeType: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
