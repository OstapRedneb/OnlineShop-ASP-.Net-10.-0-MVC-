namespace OnlineShop.Info
{
    public static class Info
    {
        public static Guid CommonCartId { get; set; } = Guid.NewGuid();
        public static Guid CommonFavoriteId { get; set; } = Guid.NewGuid();
        public static Guid CommonOrderListId { get; set; } = Guid.NewGuid();
        public static Guid CommonComparatorId { get; set; } = Guid.NewGuid();
        public static Guid CommonUserId { get; set; } = Guid.NewGuid();
    }
}
