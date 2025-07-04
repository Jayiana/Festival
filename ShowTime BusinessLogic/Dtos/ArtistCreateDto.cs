using System.ComponentModel.DataAnnotations;

namespace ShowTime_BusinessLogic.Dtos
{
    public class ArtistCreateDto
    {
        [Required(ErrorMessage = "Artist name is required.")]
        [RegularExpression(@"^(?!\d+$).+", ErrorMessage = "Artist name cannot be only numbers.")]
        [MinLength(2, ErrorMessage = "Artist name must be at least 2 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Genre is required.")]
        [MinLength(2, ErrorMessage = "Genre must be at least 2 characters.")]
        [RegularExpression(@"^[\w\s&\-]+$", ErrorMessage = "Genre contains invalid characters.")]
        public string Genre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Image must be a valid URL.")]
        public string Image { get; set; } = string.Empty;
    }
}
