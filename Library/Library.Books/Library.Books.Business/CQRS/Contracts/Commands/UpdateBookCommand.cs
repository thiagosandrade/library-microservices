using System;
using System.Collections.Generic;
using Library.Books.Domain.Models;
using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Commands
{
    public class UpdateBookCommand : IRequest
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
        public IEnumerable<BookAuthor> Authors { get; set; }
        public IEnumerable<BookCategory> Categories { get; set; }
        public string User { get; set; }
    }
}
