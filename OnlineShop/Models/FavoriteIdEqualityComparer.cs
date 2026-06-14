namespace OnlineShop.Models
{
    public class FavoriteIdEqualityComparer : IEqualityComparer<Favorite>
    {
        public bool Equals(Favorite favorite1, Favorite favorite2) 
        {
            return favorite1?.Id == favorite2?.Id;
        }

        public int GetHashCode(Favorite favorite) 
        {
            return HashCode.Combine(favorite.Id);
        }
    }
}