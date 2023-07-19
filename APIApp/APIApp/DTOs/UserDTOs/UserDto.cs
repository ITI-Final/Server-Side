namespace APIApp.DTOs.UserDTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public bool? Gender { get; set; }
        public int? Company { get; set; }
        public DateTime? Birth_Date { get; set; }
        public int? Post_Count { get; set; }

        public DateTime? Register_Date { get; set; }
    }
}
