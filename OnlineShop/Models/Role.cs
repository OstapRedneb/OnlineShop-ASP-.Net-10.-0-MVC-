using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Role
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required(ErrorMessage = "Role name is required")]
        [Display(Name = "ROLE_NAME")]
        public string Name { get; init; } = "User";

        [Display(Name = "Add Products")]
        public bool CanAddProducts { get; init; } = false;

        [Display(Name = "Edit Products")]
        public bool CanEditProducts { get; init; } = false;

        [Display(Name = "Delete Products")]
        public bool CanDeleteProducts { get; init; } = false;

        [Display(Name = "Manage Users (add/remove)")]
        public bool CanManageUsers { get; init; } = false;

        [Display(Name = "Change User Roles")]
        public bool CanChangeUserRoles { get; init; } = false;

        [Display(Name = "View Orders")]
        public bool CanViewOrders { get; init; } = false;

        [Display(Name = "Change Order Status")]
        public bool CanChangeOrderStatus { get; init; } = false;

        [Display(Name = "Manage Roles (add/remove)")]
        public bool CanManageRoles { get; init; } = false;

        public bool IsAdmin => 
            CanAddProducts || 
            CanEditProducts || 
            CanDeleteProducts || 
            CanManageUsers || 
            CanChangeUserRoles || 
            CanManageRoles || 
            CanChangeOrderStatus || 
            CanViewOrders;

        public Role()
        {
            
        }
    }
}
