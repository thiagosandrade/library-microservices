using MediatR;
using System;

namespace Library.Authors.Business.CQRS.Contracts.Commands
{
    public class CreateAuthorCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public Guid PlaceOfBirthId { get; set; }
    }
}
