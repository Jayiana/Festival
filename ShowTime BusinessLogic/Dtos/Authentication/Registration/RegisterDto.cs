using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class RegisterDto : IValidatableObject
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!Regex.IsMatch(Password ?? "", @"[a-z]"))
            yield return new ValidationResult("Password must contain at least one lowercase letter.", new[] { nameof(Password) });

        if (!Regex.IsMatch(Password ?? "", @"[A-Z]"))
            yield return new ValidationResult("Password must contain at least one uppercase letter.", new[] { nameof(Password) });

        if (!Regex.IsMatch(Password ?? "", @"\d"))
            yield return new ValidationResult("Password must contain at least one digit.", new[] { nameof(Password) });

        if (!Regex.IsMatch(Password ?? "", @"[\W_]"))
            yield return new ValidationResult("Password must contain at least one special character.", new[] { nameof(Password) });
    }
}
