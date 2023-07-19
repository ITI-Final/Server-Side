using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIApp.DTOs.ChatDTOs
{
    public class ChatMessageDTO
    {
        public string Message { get; set; } = string.Empty;
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Sender_ID { get; set; }
        public int Receiver_ID { get; set; }
    }
}
