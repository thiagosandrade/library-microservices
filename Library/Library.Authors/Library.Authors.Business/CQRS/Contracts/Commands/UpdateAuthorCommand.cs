using System;
using MediatR;

namespace Library.Authors.Business.CQRS.Contracts.Commands
{
    public class UpdateAuthorCommand : IRequest
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public Guid PlaceOfBirthId { get; set; }
    }
}
