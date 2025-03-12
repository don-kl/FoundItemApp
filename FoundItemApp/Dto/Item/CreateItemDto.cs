using FoundItemApp.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoundItemApp.Dto.Item
{
    public class CreateItemDto
    {
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Cannot exceed more than 500 words")]
        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 5, ErrorMessage = "Cannot exceed 50 words")]
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Location { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Required a valid email address")]
        [Required]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [Precision(2, 20)]
        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Required]
        [Precision(2, 20)]
        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Required]
        public string RegionName { get; set; } = string.Empty;

        [Required]
        public ItemCategory Category { get; set; }

        [Required]
        public DateOnly DateFound { get; set; }

        [Required]
        public TimeOnly TimeFound { get; set; }

        public List<IFormFile> Pictures { get; set; } = new List<IFormFile>();
    }
}
