using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public record User
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Login name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Login should be longer than {2} and shourter than {1}")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password should be longer than {2} and shourter than {1}")]
        [DataType(DataType.Password)]
        [Display(Name = "PASSWORD")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone name is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "PHONE")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be longer than {2} and shourter than {1}")]
        [Display(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "LastName should be longer than {2} and shourter than {1}")]
        [Display(Name = "LAST_NAME")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email name is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "EMAIL")]
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid RoleId { get; set; }
        public Guid CartId { get; set; }
        public Guid FavoriteId { get; set; }
        public Guid OrderListId { get; set; }
        public Guid ComparatorId { get; set; }

        public User() : this("Unknow", "password")
        { }
        public User(string login, string password) : this(login, password, "NOT_DETECTED", "NOT_DETECTED", "NOT_DETECTED", "NOT_DETECTED")
        { }
        public User(string login, string password, string phone, string firstName, string lastName, string email)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
            Phone = phone;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedAt = DateTime.Now;
        }
    }
}
