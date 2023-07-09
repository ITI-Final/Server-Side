
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
            CreateMap<CompanyDTO, Company>();
            CreateMap<UserDto, User>();
            CreateMap<UserLoginDTO, User>();

        }
    }
}
