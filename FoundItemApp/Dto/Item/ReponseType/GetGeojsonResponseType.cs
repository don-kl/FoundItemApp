using FoundItemApp.Models.Enums;

namespace FoundItemApp.Dto.Item.ReponseType
{
    public class GetGeojsonResponseType
    {
        public string Type { get; set; } = string.Empty;
        public GeometryRT? Geometry { get; set; }
        public PropertiesRT? Properties { get; set; }
    }

    public class GeometryRT
    {
        public string Type { get; set; } = string.Empty;
        public double[]? Coordinates { get; set; }
    }

    public class PropertiesRT
    {
        public Guid Id { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateOnly? Date { get; set; }

    }
}
