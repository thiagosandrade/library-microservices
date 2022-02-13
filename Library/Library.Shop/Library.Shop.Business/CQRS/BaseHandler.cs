using AutoMapper;
using Library.Shop.Database.Interfaces;
using Library.Shop.Domain.Models;

namespace Library.Shop.Business.CQRS
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
