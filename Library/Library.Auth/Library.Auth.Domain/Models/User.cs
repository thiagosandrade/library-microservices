using System;

namespace Library.Auth.Domain.Models
{
    public class User : Entity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }


        protected User()
        {

        }

        public User(string name, string surname, string login, string password, string email, Guid? id = null)
        {
            Id = id ?? Guid.NewGuid();
            Name = name;
            Surname = surname;
            Login = login;
            Password = password;
            Email = email;
        }
    }
}
