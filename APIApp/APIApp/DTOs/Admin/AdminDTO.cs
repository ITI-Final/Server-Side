namespace APIApp.DTOs.Admin
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool? Gender { get; set; }
        public string Phone { get; set; } = string.Empty;
        public DateTime? Birth_Date { get; set; }

        public DateTime? Added_Date { get; set; }

        public virtual ICollection<PermissionDTO> Permissions { get; set; }

    }
}
