using AutoMapper;

namespace Library.Authors.Business.CQRS
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
