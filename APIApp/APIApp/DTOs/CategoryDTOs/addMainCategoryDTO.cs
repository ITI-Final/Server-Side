namespace APIApp.DTOs.CategoryDTOs
{
    public class addMainCategoryDTO
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
    }
}
