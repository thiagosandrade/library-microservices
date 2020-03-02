using System.Collections.Generic;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Domain.Models;

namespace Library.Books.Business.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<Category, GetCategoryResult>();

            CreateMap<Book, GetBookQueryResult>()
                .ForMember(map => map.Categories, 
                    opt => opt.MapFrom(s => s.Categories));

            CreateMap<List<Book>, GetAllBookQueryResult>()
                .ForMember(map => map.Books,
                    opt => opt.MapFrom(x => x));

        }
    }
}
