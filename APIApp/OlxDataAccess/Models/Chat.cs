﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace OlxDataAccess.Models
{
    [Table("Chat")]
    public partial class Chat
    {
        public Chat()
        {
            Chat_Messages = new HashSet<Chat_Message>();
        }

        [Key]
        public int Id { get; set; }
        public int User_One { get; set; }
        public int User_Two { get; set; }
        public bool? Block { get; set; }

        [ForeignKey("User_One")]
        [InverseProperty("ChatUser_OneNavigations")]
        public virtual User User_OneNavigation { get; set; }
        [ForeignKey("User_Two")]
        [InverseProperty("ChatUser_TwoNavigations")]
        public virtual User User_TwoNavigation { get; set; }
        [InverseProperty("Chat")]
        public virtual ICollection<Chat_Message> Chat_Messages { get; set; }
    }
}