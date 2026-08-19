namespace OnlineShop.Models
{
    public record User
    {
        public Guid Id { get; init; }
        public string Login { get; init; }
        public string Password { get; init; }

        public Guid RoleId { get; set; }
        public Guid CartId { get; set; }
        public Guid FavoriteId { get; set; }
        public Guid OrderListId { get; set; }
        public Guid ComparatorId { get; set; }

        public User() : this("Unknow", "password")
        { }
        public User(string login, string password)
        {
            Id = Guid.NewGuid();
            Login = login;
            Password = password;
        }
    }
}
