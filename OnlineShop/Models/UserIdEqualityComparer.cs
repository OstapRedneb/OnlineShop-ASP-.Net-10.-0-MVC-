namespace OnlineShop.Models
{
    public class UserIdEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User user1, User user2) => user1?.Id == user2?.Id;
        public int GetHashCode(User user) => HashCode.Combine(user?.Id);
    }
}
