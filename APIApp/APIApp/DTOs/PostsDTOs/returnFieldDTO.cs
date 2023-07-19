namespace APIApp.DTOs.PostsDTOs
{
    public class returnFieldDTO
    {
        public int Field_Id { get; set; }
        public string Field_Name { get; set; }
        public string Field_Label { get; set; }
        public string Field_Label_Ar { get; set; }
        public List<returnChoicesDTO> Choices { get; set; }
    }
}
