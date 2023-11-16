namespace ContactManager.Business.DTOs.Contact
{
    public class ContactForManipulationDto
    {
        public string Name { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}