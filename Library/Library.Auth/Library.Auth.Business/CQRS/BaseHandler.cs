using AutoMapper;
using Library.Auth.Database.Interfaces;
using Library.Auth.Domain.Models;

namespace Library.Auth.Business.CQRS
{
    public class BaseHandler
    {
        protected readonly IMapper Mapper;
        protected readonly IGenericRepository<User> UserRepository;

        public BaseHandler(IMapper mapper, IGenericRepository<User> authorRepository)
        {
            Mapper = mapper;
            UserRepository = authorRepository;
        }
    }
}
