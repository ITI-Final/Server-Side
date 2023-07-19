using APIApp.DTOs.ChatDTOs;

namespace APIApp.AutoMapper
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<CategoryPostDTO, Category>();
            CreateMap<FieldPostDTO, Field>();
            CreateMap<ChoicePostDTO, Choice>();
            CreateMap<addMainCategoryDTO, Category>();
            CreateMap<GovernorateDTO, Governorate>();
            CreateMap<CitiesDTO, City>();
            CreateMap<CompanyDTO, Company>();
            CreateMap<UserDto, User>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<FavouriteDTO, Favorite>();
            CreateMap<UserRegister, User>();
            CreateMap<Governorate, GovernorateDTO>()
                    .ForMember(dest => dest.cities, opt => opt.MapFrom(src => src.Cities));
            CreateMap<City, CitiesDTO>();
            CreateMap<PostDTO, Post>();
            CreateMap<imagesDTO, Post_Image>();
            CreateMap<AdminDTO, Admin>();
            CreateMap<PermissionDTO, Permission>();
            CreateMap<ChatMessageDTO, Chat_Message>();

        }
    }
}
