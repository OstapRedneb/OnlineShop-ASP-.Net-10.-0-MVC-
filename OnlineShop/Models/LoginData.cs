using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public record LoginData()
    {
        [Display(Name = "Name", Prompt = "YOUR_NAME")]
        [Required(ErrorMessage = "Field \"Name\" is empty")]
        [StringLength(50, MinimumLength = 2, ErrorMessage ="Name should be longer than {2} and shourter than {1}")]
        [DataType(DataType.Text)]
        public string Name { get; set;  }

        [Display(Name = "Password", Prompt = "YOUR_PASSWORD")]
        [Required(ErrorMessage = "Field \"Password\" is empty")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength =6, ErrorMessage = "Password should be longer than {2} and shourter than {1}")]
        public string Password { get; set; }

        [Display(Name = "Should_Remember?", Prompt = "Should_Remember?")]
        public bool ShouldRemember {  get; set; }

        public void Deconstruct(out string name, out string password, out bool shouldRemember)
        {
            (name, password, shouldRemember) = (Name, Password, ShouldRemember);
        }
    }
}
