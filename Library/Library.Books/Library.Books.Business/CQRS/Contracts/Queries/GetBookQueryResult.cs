using Library.Books.Domain.Models;
using System;
using System.Collections.Generic;

namespace Library.Books.Business.CQRS.Contracts.Queries
{
    public class GetBookQueryResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Status { get; set; }

        public IList<GetAuthorQueryResult> Authors { get; set; }
        public IList<GetCategoryQueryResult> Categories { get; set; }
    }
}
