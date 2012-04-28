namespace ServiceInterface
{
    public interface IUserRepository
    {
        bool NameInUse(string name);
        bool Match(string username, string password);
        User CreateUser(User user);
    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}