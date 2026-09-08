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

        [Display(Name = "FIRSTNAME", Prompt = "YOUR_FIRSTNAME")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be longer than {2} and shourter than {1}")]
        [Required(ErrorMessage = "Field \"First_Name\" is empty")]
        public string FirstName { get; set; }

        [Display(Name = "LASTNAME", Prompt = "YOUR_LASTNAME")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "LastName should be longer than {2} and shourter than {1}")]
        [Required(ErrorMessage = "Field \"Last_Name\" is empty")]
        public string LastName { get; set; }

        [Display(Name = "EMAIL", Prompt = "YOUR_EMAIL")]
        [Required(ErrorMessage = "Field \"Email\" is empty")]
        [DataType(DataType.EmailAddress, ErrorMessage = "IT'S NOT EMAIL")]
        public string Email { get; set; }

        [Display(Name = "PHONE", Prompt = "YOUR_PHONE")]
        [Required(ErrorMessage = "Field \"Phone\" is empty")]
        public string Phone { get; set; }

        public void Deconstruct(
            out string name,  
            out string password,  
            out string copyPassword, 
            out string firstName, 
            out string lastName, 
            out string email, 
            out string phone) 
        {
            (name, password, copyPassword, firstName, lastName, email, phone) = (Name, Password, CopyPassword, FirstName, LastName, Email, Phone);
        }
    }
}
