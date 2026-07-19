using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public record RegisterData
    {
        [Display(Name = "Name", Prompt = "YOUR_NAME")]
        [Required(ErrorMessage = "Field \"Name\" is empty")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be longer than {2} and shourter than {1}")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Password", Prompt = "YOUR_PASSWORD")]
        [Required(ErrorMessage = "Field \"Password\" is empty")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be longer than {2} and shourter than {1}")]
        public string Password { get; set; }

        [Display(Name = "Copy_Password", Prompt = "COPY_PASSWORD")]
        [Required(ErrorMessage = "Field \"Copy_Password\" is empty")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Copy_Password should be copy of field \"Password\"")]
        public string CopyPassword { get; set; }
    }
}
