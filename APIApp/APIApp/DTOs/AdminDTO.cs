namespace APIApp.DTOs
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; }
        public DateTime? Birth_Date { get; set; }

        public DateTime? Added_Date { get; set; }

        public virtual ICollection<PermissionDTO> Permissions { get; set; }

    }
}
