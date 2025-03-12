namespace FoundItemApp.Dto.Region.ResponseType
{

    public class GetRegionResponseType
    {
        public string? Type { get; set; }
        public GeometryRT? Geometry { get; set; } = new GeometryRT();
        public PropertiesRT? Properties { get; set; } = new PropertiesRT();
    }

    public class GeometryRT
    {
        public string? Type { get; set; }
        public Double[]? Coordinates { get; set; }
    }

    public class PropertiesRT
    {
        public string? Name { get; set; }
        public Double[]? Envelope { get; set; }

    }
}
