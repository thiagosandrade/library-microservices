using MediatR;
using System;

namespace Library.Books.Business.CQRS.Contracts.Commands
{
    public class CreateAuthorCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
    }
}
