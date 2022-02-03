using System.Collections.Generic;

namespace Library.Auth.Business.CQRS.Contracts.Queries
{
    public class GetAllUserWithRolesQueryResult
    {
        public IEnumerable<GetUserWithRolesQueryResult> Users { get; set; }
    }
}