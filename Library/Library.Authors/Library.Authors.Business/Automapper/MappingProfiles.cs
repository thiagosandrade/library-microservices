﻿using System.Collections.Generic;
using AutoMapper;
using Library.Authors.Business.CQRS.Contracts.Queries;
using Library.Authors.Domain.Models;

namespace Library.Authors.Business.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            
            CreateMap<PlaceOfBirth, GetPlaceOfBirthResult>();

            CreateMap<Author, GetAuthorQueryResult>()
                .ForMember(map => map.PlaceOfBirth, 
                    opt => opt.MapFrom(s => s.PlaceOfBirth));

            CreateMap<List<Author>, GetAllAuthorQueryResult>()
                .ForMember(map => map.Authors,
                    opt => opt.MapFrom(x => x));
        }
    }
}
