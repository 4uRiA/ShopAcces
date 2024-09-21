using Microsoft.AspNetCore.Identity;

namespace ShopAcces.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public Accessores Accessories { get; set; }
        public IdentityUser User { get; set; }
        public int amount { get; set; }
    }
}
