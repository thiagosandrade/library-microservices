using Library.Authors.Domain.Models;
using Library.Authors.Rabbit.RabbitMq;
using MediatR;
using System;

namespace Library.Authors.Business.Events
{
    public class AuthorCreatedEvent : MessageEvent, IRequest<Unit>
    {
        public Author Author { get; set; }
        public DateTime MessageDate { get; set; }

        public AuthorCreatedEvent(Author author, DateTime messageDate)
        {
            Author = author;
            MessageDate = messageDate;
        }
    }
}
