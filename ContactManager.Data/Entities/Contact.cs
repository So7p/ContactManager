namespace ContactManager.Data.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string MobilePhone { get; set; } = null!;
        public string JobTitle { get; set; } = null!;
        public DateTime BirthDate { get; set; }
    }
}