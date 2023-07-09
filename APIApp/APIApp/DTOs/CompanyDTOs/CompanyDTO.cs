namespace APIApp.DTOs.CompanyDTOs
{
    using System.ComponentModel;

    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Logo_Url { get; set; }
        public string Cover_Url { get; set; }
        public string Tax_Number { get; set; }
        //user id
       
        public int OwnerID { get; set; }
   
        public DateTime? Register_Date { get; set; }
    }
}
