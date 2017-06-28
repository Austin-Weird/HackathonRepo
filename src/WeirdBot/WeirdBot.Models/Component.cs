namespace WeirdBot.Models
{
    public class Component
    {
        public int ID { get; set; }
        public ComponentType Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Quality Quality { get; set; }
        public string VendorUrl { get; set; }
    }
}
