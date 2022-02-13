using System;
using MediatR;

namespace Library.Books.Business.CQRS.Contracts.Commands
{
    public class UpdateAuthorCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}
