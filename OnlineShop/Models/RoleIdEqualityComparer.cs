namespace OnlineShop.Models
{
    public class RoleIdEqualityComparer : IEqualityComparer<Role>
    {
        public bool Equals(Role role1, Role role2) => role1?.Id == role2?.Id;
        public int GetHashCode(Role role) => HashCode.Combine(role?.Id);
    }
}