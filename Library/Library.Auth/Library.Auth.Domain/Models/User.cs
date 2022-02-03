using System.Collections.Generic;

namespace Library.Auth.Domain.Models
{
    public class User : Entity
    {
        public User(string name, string surname, string login, string password, string email)
        {
            Name = name;
            Surname = surname;
            Login = login;
            Password = password;
            Email = email;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public virtual IEnumerable<UserRole> UserRoles { get; set; }
    }
}
