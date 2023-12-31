﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace OlxDataAccess.Models
{
    [Table("Post_Image")]
    [Index("Post_Id", Name = "IX_Post_Image_Post_Id")]
    public partial class Post_Image
    {
        [Key]
        public int Id { get; set; }
        public int Post_Id { get; set; }
        [Required]
        public string Image { get; set; }

        [ForeignKey("Post_Id")]
        [InverseProperty("Post_Images")]
        public virtual Post Post { get; set; }
    }
}