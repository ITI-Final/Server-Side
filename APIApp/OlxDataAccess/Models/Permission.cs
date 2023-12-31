﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace OlxDataAccess.Models
{
    [Table("Permission")]
    public partial class Permission
    {
        public Permission()
        {
            Admins = new HashSet<Admin>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Section { get; set; }
        public bool Can_View { get; set; }
        public bool Can_Add { get; set; }
        public bool Can_Edit { get; set; }
        public bool Can_Delete { get; set; }

        [ForeignKey("Permission_Id")]
        [InverseProperty("Permissions")]
        public virtual ICollection<Admin> Admins { get; set; }
    }
}