namespace OnlineShop.Models
{
    public record User
    {
        public Guid Id { get; init; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
