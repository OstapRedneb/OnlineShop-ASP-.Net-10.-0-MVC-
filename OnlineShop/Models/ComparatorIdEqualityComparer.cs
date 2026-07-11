namespace OnlineShop.Models
{
    public class ComparatorIdEqualityComparer : IEqualityComparer<Comparator>
    {
        public bool Equals(Comparator comparator1, Comparator comparator2) 
        {
            return comparator1?.Id == comparator2?.Id;
        }

        public int GetHashCode(Comparator comparator) 
        {
            return HashCode.Combine(comparator.Id);
        }
    }
}