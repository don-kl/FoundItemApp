using FoundItemApp.Models.Enums;

namespace FoundItemApp.Dto.Item
{
    public class ItemDto

    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; }
        public string Status { get; set; }
        public DateOnly DateFound { get; set; }
        public string RegionName {  get; set; } = string.Empty;
    }
}
