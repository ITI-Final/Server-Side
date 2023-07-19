﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable

namespace OlxDataAccess.Models
{
    [Table("Chat_Message")]
    public partial class Chat_Message
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Sender_ID { get; set; }
        public int Receiver_ID { get; set; }

        [ForeignKey("Receiver_ID")]
        [InverseProperty("Chat_MessageReceivers")]
        public virtual User Receiver { get; set; }
        [ForeignKey("Sender_ID")]
        [InverseProperty("Chat_MessageSenders")]
        public virtual User Sender { get; set; }
    }
}