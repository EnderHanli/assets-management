using Domain.Common;

namespace Domain.Entities
{
    public class StatusType : Enumeration
    {
        public static readonly StatusType Deployable = new(1, "Deployable");
        public static readonly StatusType Pending = new(2, "Pending");
        public static readonly StatusType Undeployable = new(3, "Undeployable");
        public static readonly StatusType Archived = new(4, "Archived");

        private StatusType(int id, string name)
        : base(id, name)
        {
        }

        public static IEnumerable<StatusType> List() =>
            new[] { Deployable, Pending, Undeployable, Archived };

        public static StatusType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for StatusType: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static StatusType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for StatusType: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
