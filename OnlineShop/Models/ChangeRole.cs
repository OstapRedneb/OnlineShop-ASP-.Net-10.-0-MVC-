using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ChangeRole
    {
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Role is required")]
        [Display(Name = "ROLE")]
        public Guid RoleId { get; set; }
    }
}
