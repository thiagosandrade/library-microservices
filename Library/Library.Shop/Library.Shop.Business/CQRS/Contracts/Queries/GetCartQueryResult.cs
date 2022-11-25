using System;
using System.Collections.Generic;

namespace Library.Shop.Business.CQRS.Contracts.Queries
{
    public class GetCartQueryResult
    {
        public int Id { get; set; }
     
        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<GetProductCartQueryResult> Items { get; set; }
    }
}