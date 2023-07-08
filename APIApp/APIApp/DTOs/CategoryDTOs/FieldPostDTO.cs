namespace APIApp.DTOs.CategoryDTOs
{
    public class FieldPostDTO
    {
        public int Id { get; set; }
        public int Cat_Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Label_Ar { get; set; }
        public string Value_Type { get; set; }
        public string Choice_Type { get; set; }
        public int? Max_Length { get; set; }
        public int? Min_Length { get; set; }

        public int? Max_Value { get; set; }
        public int? Min_Value { get; set; }
        public bool Is_Required { get; set; }
        public int? Parent_Id { get; set; }
        public List<ChoicePostDTO> Choices { get; set; }
    }
}
