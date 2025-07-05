using ShowTime.DataAccess.Models.ArtistInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime_BusinessLogic.Dtos.Artist
{
    public class ArtistUpdateDto
    {
        [Required(ErrorMessage = "Artist name is required.")]
        [RegularExpression(@"^(?!\\d+$).+", ErrorMessage = "Artist name cannot be only numbers.")]
        [MinLength(2, ErrorMessage = "Artist name must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Image must be a valid URL.")]
        public string Image { get; set; } = string.Empty;

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; } = 0.0;

        public bool IsTrending { get; set; } = false;

        public string? TrendingReason { get; set; }

        public string? Origin { get; set; }

        [StringLength(1000, ErrorMessage = "Description is too long (max 1000 characters).")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Instagram must be a valid URL.")]
        public string? Instagram { get; set; }

        [Url(ErrorMessage = "YouTube must be a valid URL.")]
        public string? YouTube { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "FansCount must be a positive number.")]
        public int FansCount { get; set; } = 0;

        [Range(1900, 2100, ErrorMessage = "DebutYear must be between 1900 and 2100.")]
        public int DebutYear { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public ArtistCategory Category { get; set; }
    }
}
