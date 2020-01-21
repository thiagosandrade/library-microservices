using AutoMapper;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;

namespace Library.Authors.Business.CQRS
{
    public class BaseHandler
    {
        protected readonly IMapper Mapper;
        protected readonly IGenericRepository<Author> AuthorRepository;

        public BaseHandler(IMapper mapper, IGenericRepository<Author> authorRepository)
        {
            Mapper = mapper;
            AuthorRepository = authorRepository;
        }

    }
}
