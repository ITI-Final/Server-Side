namespace APIApp.DTOs.CategoryDTOs
{
    public class CategoryPostDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Label_Ar { get; set; }
        public int? Parent_Id { get; set; }
        public string Slug { get; set; }
        public string? Description { get; set; }
        public string? Tags { get; set; }
        public string? Icon { get; set; }
        public int? Admin_Id { get; set; }
        public DateTime? Created_Date { get; set; }
        public List<FieldPostDTO> Fields { get; set; }
    }
}


//.ForMember(
//                destinationMember => destinationMember.Id,
//                opt => opt.MapFrom(src => $"{src.Id}")
//                ).ForMember(
//                destinationMember => destinationMember.Name,
//                opt => opt.MapFrom(src => $"{src.Name}")

//                ).ForMember(
//                destinationMember => destinationMember.Label,
//                opt => opt.MapFrom(src => $"{src.Label}")

//                ).ForMember(
//                destinationMember => destinationMember.Label_Ar,
//                opt => opt.MapFrom(src => $"{src.Label_Ar}")

//                ).ForMember(
//                destinationMember => destinationMember.Parent_Id,
//                opt => opt.MapFrom(src => $"{src.Parent_Id}")

//                )
//                .ForMember(
//                destinationMember => destinationMember.Slug,
//                opt => opt.MapFrom(src => $"{src.Slug}")

//                )
//                .ForMember(
//                destinationMember => destinationMember.Description,
//                opt => opt.MapFrom(src => $"{src.Description}")

//                )
//                  .ForMember(
//                destinationMember => destinationMember.Tags,
//                opt => opt.MapFrom(src => $"{src.Tags}")

//                ).ForMember(
//                destinationMember => destinationMember.Admin_Id,
//                opt => opt.MapFrom(src => $"{src.Admin_Id}")

//                ).ForMember(
//                destinationMember => destinationMember.Created_Date,
//                opt => opt.MapFrom(src => $"{src.Created_Date}")

//                ).ForMember(
//                destinationMember => destinationMember.Fields,
//                opt => opt.MapFrom(src => $"{src.Fields}")

//                );