using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShowTime.DataAccess.Models.AccommodationInfo
{
    public class AccommodationTypeInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Type { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string FeaturesSerialized { get; set; } = string.Empty;
        [NotMapped]
        public string[] Features
        {
            get => FeaturesSerialized.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            set => FeaturesSerialized = string.Join(",", value);
        }
    }
} 