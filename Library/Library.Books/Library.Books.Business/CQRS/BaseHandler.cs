using AutoMapper;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;

namespace Library.Books.Business.CQRS
{
    public class BaseHandler<T> where T : Entity
    {
        protected readonly IMapper Mapper;
        protected readonly IGenericRepository<T> Repository;

        public BaseHandler(IMapper mapper, IGenericRepository<T> repository)
        {
            Mapper = mapper;
            Repository = repository;
        }
    }
}
