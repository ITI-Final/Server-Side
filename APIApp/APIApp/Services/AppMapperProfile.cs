using APIApp.DTOs.CategoryDTOs;
using AutoMapper;
using OlxDataAccess.Models;

namespace APIApp.Services
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<CategoryPostDTO, Category>();
            CreateMap<FieldPostDTO, Field>();
            CreateMap<ChoicePostDTO, Choice>();
            CreateMap<addMainCategoryDTO, Category>();

        }
    }
}
