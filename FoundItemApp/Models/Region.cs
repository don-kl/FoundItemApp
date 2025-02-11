using NetTopologySuite.Geometries;

namespace FoundItemApp.Models
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Geometry? Borders { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}
