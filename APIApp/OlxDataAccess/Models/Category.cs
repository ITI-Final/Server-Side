﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace OlxDataAccess.Models
{
    [Table("Category")]
    [Index("Admin_Id", Name = "IX_Category_Admin_Id")]
    [Index("Parent_Id", Name = "IX_Category_Parent_Id")]
    public partial class Category
    {
        public Category()
        {
            Fields = new HashSet<Field>();
            InverseParent = new HashSet<Category>();
            Posts = new HashSet<Post>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Label { get; set; }
        [Required]
        [StringLength(50)]
        public string Label_Ar { get; set; }
        public int? Parent_Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int? Admin_Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Created_Date { get; set; }

        [ForeignKey("Admin_Id")]
        [InverseProperty("Categories")]
        public virtual Admin Admin { get; set; }
        [ForeignKey("Parent_Id")]
        [InverseProperty("InverseParent")]
        public virtual Category Parent { get; set; }
        [InverseProperty("Cat")]
        public virtual ICollection<Field> Fields { get; set; }
        [InverseProperty("Parent")]
        public virtual ICollection<Category> InverseParent { get; set; }
        [InverseProperty("Cat")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}