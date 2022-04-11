
using System.Collections.Generic;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAllBookQueryResult
    {
        public List<GetBookQueryResult> Books { get; set; }
    }
}