using Domain.Common;

namespace Domain.Entities
{
    public class CategoryType : Enumeration
    {
        public static readonly CategoryType Acceessory = new(1, "Accessory");
        public static readonly CategoryType Asset = new(2, "Asset");
        public static readonly CategoryType Consumable = new(3, "Consumable");
        public static readonly CategoryType Component = new(4, "Component");
        public static readonly CategoryType Licence = new(5, "Licence");

        private CategoryType(int id, string name)
        : base(id, name)

        {
        }

        public static IEnumerable<CategoryType> List() =>
            new[] { Acceessory, Asset, Consumable, Component, Licence };

        public static CategoryType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new ArgumentException($"Possible values for CategoryType: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static CategoryType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new ArgumentException($"Possible values for CategoryType: {string.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
