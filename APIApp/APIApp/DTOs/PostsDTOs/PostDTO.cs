namespace APIApp.DTOs.PostsDTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public int Cat_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public int? Price_Type { get; set; }
        public int Contact_Method { get; set; }
        public int? Post_Location { get; set; }
        public DateTime? Created_Date { get; set; }

        public bool? Is_Visible { get; set; }
        public int Views { get; set; }
        public bool Is_Special { get; set; }

        public string Fields { get; set; }

        // public List<imagesDTO> Post_Images { get; set; }
    }
}
