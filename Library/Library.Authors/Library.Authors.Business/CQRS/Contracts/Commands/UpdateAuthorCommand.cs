using System;
using Library.Authors.Domain.Models;
using MediatR;

namespace Library.Authors.Business.CQRS.Contracts.Commands
{
    public class UpdateAuthorCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public Guid PlaceOfBirthId { get; set; }
        public PlaceOfBirth PlaceOfBirth { get; set; }

    }
}
