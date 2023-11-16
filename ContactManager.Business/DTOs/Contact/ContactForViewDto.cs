namespace ContactManager.Business.DTOs.Contact
{
    public class ContactForViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public DateOnly BirthDate { get; set; }
    }
}