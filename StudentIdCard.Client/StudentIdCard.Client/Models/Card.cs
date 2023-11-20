namespace StudentIdCard.Client.Models
{
    public class Card
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string ParentName { get; set; }
        public long ParentPhoneNumber { get; set; }
        public string SchoolId { get; set; }
        public decimal Balance { get; set; }
        public string ParentUserName { get; set; }
        public string ParentPassword { get; set; }
        public string? PhotoPath { get; set; }
    }
}
