using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIApp.DTOs.PostsDTOs
{
    public class imagesDTO
    {
        public int Id { get; set; }
        public int Post_Id { get; set; }
        public IFormFile ImageFile { get; set; }
        public string? Image { get; set; }
    }
}
