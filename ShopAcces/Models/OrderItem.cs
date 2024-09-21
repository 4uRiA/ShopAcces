namespace ShopAcces.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Accessores Accessores { get; set; }
        public int amount { get; set; }
    }
}
