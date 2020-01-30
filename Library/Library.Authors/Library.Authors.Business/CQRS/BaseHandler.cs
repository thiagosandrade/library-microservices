using AutoMapper;
using Library.Authors.Database.Interfaces;
using Library.Authors.Domain.Models;

namespace Library.Authors.Business.CQRS
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
