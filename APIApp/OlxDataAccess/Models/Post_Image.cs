﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace OlxDataAccess.Models
{
    [Table("Post_Image")]
    public partial class Post_Image
    {
        [Key]
        public int Id { get; set; }
        public int Post_Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Image { get; set; }

        [ForeignKey("Post_Id")]
        [InverseProperty("Post_Images")]
        public virtual Post Post { get; set; }
    }
}