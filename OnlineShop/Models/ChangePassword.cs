using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ChangePassword
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [Display(Name = "NEW_PASSWORD", Prompt = "NEW_PASSWORD")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "CONFIRM_PASSWORD", Prompt = "CONFIRM_PASSWORD")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
