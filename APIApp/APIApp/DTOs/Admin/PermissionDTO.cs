namespace APIApp.DTOs.Admin
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Section { get; set; }
        public bool Can_View { get; set; }
        public bool Can_Add { get; set; }
        public bool Can_Edit { get; set; }
        public bool Can_Delete { get; set; }
    }
}
