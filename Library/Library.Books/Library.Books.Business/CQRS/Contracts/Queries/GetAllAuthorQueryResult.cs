using System.Collections.Generic;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAllAuthorQueryResult
    {
        public List<GetAuthorQueryResult> Authors { get; set; }
    }
}