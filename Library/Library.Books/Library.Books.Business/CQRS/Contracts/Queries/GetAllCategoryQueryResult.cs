
using System.Collections.Generic;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetAllCategoryQueryResult
    {
        public List<GetCategoryQueryResult> Categories { get; set; }
    }
}