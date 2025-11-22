using Application.DTOs.Pet;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<CreatePetDto, Pet>()
                .ForMember(dest => dest.Type,
                    opt => opt.MapFrom(src => (PetType)Enum.ToObject(typeof(PetType), src.Type.Id)))
                .ForMember(dest => dest.Breed,
                    opt => opt.MapFrom(src => (PetBreed)Enum.ToObject(typeof(PetBreed), src.Breed.Id)));

            //CreateMap<PetImageDto, PetImages>();

            CreateMap<PetDto, Pet>().ReverseMap();
            CreateMap<PetBreedDto, PetBreed>().ReverseMap();
            CreateMap<PetTypeDto, PetType>().ReverseMap();
            CreateMap<PetImagesDto, PetImages>().ReverseMap();
            CreateMap<PetFavoritesDto, PetFavorite>().ReverseMap();
        }
    }
}
