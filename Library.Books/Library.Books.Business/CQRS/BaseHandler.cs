using AutoMapper;

namespace Library.Books.Business.CQRS
{
    public class BaseHandler
    {
        protected readonly IMapper Mapper;

        public BaseHandler(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}
