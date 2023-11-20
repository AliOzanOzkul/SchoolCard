namespace StudentIdCard.Client.Models
{
    public class ProductView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stoct { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
