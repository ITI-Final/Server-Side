namespace APIApp.DTOs
{
    public class AdminLoginDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PermissionDTO> Permissions { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Email: {Email}, Name: {Name}, Permissions: [{string.Join(", ", Permissions)}]";
        }
    }
}