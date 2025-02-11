using NetTopologySuite.Geometries;

namespace FoundItemApp.Models
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public Geometry? Coordinates { get; set; }
        public ItemCategory Category { get; set; }
        public ItemStatus Status { get; set; }
        public DateOnly DateFound { get; set; }
        public TimeOnly TimeFound { get; set; }
        public DateTime PostedDate { get; set; }
        public List<string>? Images { get; set; } = new List<string>();
        public Guid? RegionId { get; set; }
        public Region? Region { get; set; }
    }
}
