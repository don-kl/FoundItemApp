using FoundItemApp.Models.Enums;

namespace FoundItemApp.Dto.Region.ResponseType
{

    public class GetRegionResponseType
    {
        public string Type { get; set; } = string.Empty;
        public PropertiesGRRT? Geometry { get; set; } = new PropertiesGRRT();
        public GeometryGRRT? Properties { get; set; } = new GeometryGRRT();
    }

    public class GeometryGRRT
    {
        public string Type { get; set; } = string.Empty;
        public double[]? Coordinates { get; set; }
    }

    public class PropertiesGRRT
    {
        public string Id { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateOnly Date { get; set; }

    }
}
