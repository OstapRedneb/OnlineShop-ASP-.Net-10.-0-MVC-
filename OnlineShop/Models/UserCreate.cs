using Microsoft.AspNetCore.Cors.Infrastructure;
using OnlineShop.Services.Interfaces;
using OnlineShop.Services.JsonServices;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace OnlineShop.Models 
{ 
    public class UserCreate
    {
        [Required(ErrorMessage = "Login is required")]
        [Display(Name = "LOGIN")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "EMAIL")]
        public string Email { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "FIRST_NAME")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "LAST_NAME")]
        public string LastName { get; set; }

        [Display(Name = "PHONE")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [Display(Name = "PASSWORD", Prompt = "••••••••")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "CONFIRM_PASSWORD", Prompt = "••••••••")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "ROLE")]
        public Guid RoleId { get; set; }

        
        public static explicit operator User(UserCreate userCreate) 
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Login = userCreate.Login,
                Password = userCreate.Password,
                CreatedAt = DateTime.UtcNow,
                Email = userCreate.Email,
                FirstName = userCreate.FirstName,
                LastName = userCreate.LastName,
                Phone = userCreate.Phone,
                RoleId = userCreate.RoleId
            };

            return user;
        }
    }
}