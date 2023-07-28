namespace APIApp.DTOs.CategoryDTOs
{
    public class GetCatNameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Label_Ar { get; set; }
        public string Icon { get; set; }
        public string Slug { get; set; }
        public int? Parent { get; set; }
    }
}
