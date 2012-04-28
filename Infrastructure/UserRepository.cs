namespace Infrastructure
{
    using System;
    using System.Collections.Generic;
    using ServiceInterface;

    public class UserRepository : IUserRepository
    {
        private Dictionary<string, string> Users = new Dictionary<string, string>();

        public bool NameInUse(string name)
        {
            return Users.ContainsKey(name);
        }

        public bool Match(string username, string password)
        {
            return Users[username] == password;
        }

        public User CreateUser(User user)
        {
            Console.WriteLine(string.Format("{0} was created", user.Username));
            Users[user.Username] = user.Password;
            return user;
        }
    }
}