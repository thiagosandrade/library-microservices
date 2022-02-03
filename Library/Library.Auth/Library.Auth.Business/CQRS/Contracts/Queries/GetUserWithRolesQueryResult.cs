using System.Collections.Generic;

namespace Library.Auth.Business.CQRS.Contracts.Queries
{
    public class GetUserWithRolesQueryResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<UserRolesResult> UserRoles { get; set; }
    }
}