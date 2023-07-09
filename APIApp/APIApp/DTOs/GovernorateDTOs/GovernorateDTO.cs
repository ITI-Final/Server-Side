namespace APIApp.DTOs.GovernorateDTOs
{
    public class GovernorateDTO
    {
        public int Id { get; set; }
        public string Governorate_Name_Ar { get; set; }
        public string Governorate_Name_En { get; set; }
        public List<CitiesDTO> cities { get; set; }
    }
}
