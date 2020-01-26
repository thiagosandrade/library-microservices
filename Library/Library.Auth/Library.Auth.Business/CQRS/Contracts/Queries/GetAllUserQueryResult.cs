using System.Collections.Generic;

namespace Library.Auth.Business.CQRS.Contracts.Queries
{
    public class GetAllUserQueryResult
    {
        public IEnumerable<GetUserQueryResult> Users { get; set; }
    }
}