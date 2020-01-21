using MediatR;
using System;
using System.Threading.Tasks;

namespace Library.Authors.Business.CQRS.Contracts.Commands
{
    public class CreateAuthorCommand : IRequest
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birth { get; set; }
        public int PlaceOfBirthId { get; set; }
    }
}
