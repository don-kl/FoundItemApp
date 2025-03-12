using FoundItemApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace FoundItemApp.Dto.Item
{
    public class GetItemByIdDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public ItemCategory Category { get; set; }
        public DateOnly DateFound { get; set; }
        public TimeOnly TimeFound { get; set; }
        public string RegionName {  get; set; } = string.Empty;
        public List<string>? Pictures { get; set; }
    }
}
