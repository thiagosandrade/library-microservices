using AutoMapper;
using Library.Books.Database.Interfaces;
using Library.Books.Domain.Models;

namespace Library.Books.Business.CQRS
{
    public class BaseHandler
    {
        protected readonly IMapper Mapper;
        protected readonly IGenericRepository<Book> BookRepository;

        public BaseHandler(IMapper mapper, IGenericRepository<Book> bookRepository)
        {
            Mapper = mapper;
            BookRepository = bookRepository;
        }
    }
}
