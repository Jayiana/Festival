using System;
using System.ComponentModel.DataAnnotations;
using ShowTime.DataAccess.Models.ArtistInfo;

namespace ShowTime_BusinessLogic.Dtos.Artist
{
    public class ArtistCreateDto
    {
        [Required(ErrorMessage = "Artist name is required.")]
        [RegularExpression(@"^(?!\d+$).+", ErrorMessage = "Artist name cannot be only numbers.")]
        [MinLength(2, ErrorMessage = "Artist name must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = string.Empty; 

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Image must be a valid URL.")]
        public string Image { get; set; } = string.Empty;

        [Range(0.0, 5.0, ErrorMessage = "Rating must be between 0.0 and 5.0.")]
        public double Rating { get; set; } = 0.0;

        public bool IsTrending { get; set; } = false;

        [MaxLength(200, ErrorMessage = "Trending reason can't exceed 200 characters.")]
        public string? TrendingReason { get; set; }

        [MaxLength(100, ErrorMessage = "Origin can't exceed 100 characters.")]
        public string? Origin { get; set; }

        [MaxLength(1000, ErrorMessage = "Description can't exceed 1000 characters.")]
        public string? Description { get; set; }

        [Url(ErrorMessage = "Instagram must be a valid URL.")]
        public string? Instagram { get; set; }

        [Url(ErrorMessage = "YouTube must be a valid URL.")]
        public string? YouTube { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Fans count must be a positive number.")]
        public int FansCount { get; set; } = 0;

        [Range(1900, int.MaxValue, ErrorMessage = "Debut year must be after 1900.")]
        public int DebutYear { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public ArtistCategory Category { get; set; }
    }
}
