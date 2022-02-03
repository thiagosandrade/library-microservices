namespace Library.Auth.Domain.Models
{
    public class Role : Entity
    {
        public Role(string RoleName)
        {
            this.RoleName = RoleName;
        }

        public string RoleName { get; set; }
    }
}
