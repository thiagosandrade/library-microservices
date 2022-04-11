using System.Collections.Generic;
using AutoMapper;
using Library.Books.Business.CQRS.Contracts.Commands;
using Library.Books.Business.CQRS.Contracts.Queries;
using Library.Books.Domain.Json;
using Library.Books.Domain.Models;

namespace Library.Books.Business.Automapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<Category, GetCategoryQueryResult>();
            
            CreateMap<List<Category>, GetAllCategoryQueryResult>()
                .ForMember(map => map.Categories,
                    opt => opt.MapFrom(x => x));

            CreateMap<Book, GetBookQueryResult>()
                .ForMember(map => map.Categories,
                    opt => opt.MapFrom(x => x.Categories))
                .ForMember(map => map.Authors,
                    opt => opt.MapFrom(x => x.Authors));

            CreateMap<List<Book>, GetAllBookQueryResult>()
                .ForMember(map => map.Books,
                    opt => opt.MapFrom(x => x));

            CreateMap<Author, GetAuthorQueryResult>();

            CreateMap<List<Author>, GetAllAuthorQueryResult>()
                .ForMember(map => map.Authors,
                    opt => opt.MapFrom(x => x));


            CreateMap<BookAuthor, GetAuthorQueryResult>()
                .IncludeMembers(x => x.Author);

            CreateMap<BookCategory, GetCategoryQueryResult>()
                .IncludeMembers(x => x.Category);

            CreateMap<CreateAuthorCommand, Author>();
            CreateMap<UpdateAuthorCommand, Author>();

            CreateMap<CreateBookCommand, Book>();
            CreateMap<UpdateBookCommand, Book>();

        }
    }
}
