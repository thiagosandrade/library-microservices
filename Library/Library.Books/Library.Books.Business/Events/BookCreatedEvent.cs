using Library.Books.Domain.Models;
using Library.Hub.Rabbit.RabbitMq;
using MediatR;
using System;

namespace Library.Books.Business.Events
{
    public class BookCreatedEvent : MessageEvent, IRequest<Unit>
    {
        public Book Book { get; }
        public DateTime MessageDate { get; set; }

        public BookCreatedEvent(Book book, DateTime date)
        {
            Book = book;
            MessageDate = date;
        }
    }
}
