using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAcces.Data;
using ShopAcces.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopAcces.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _uManager;
        public CartController(ApplicationDbContext context, UserManager<IdentityUser> uManager)
        {
            _context = context;
            _uManager = uManager;
        }

        [HttpPost]
        public async Task<IActionResult> PushToCart([FromBody] int accesId)
        {
            var userId = _uManager.GetUserId(User);

            var item = _context.Cart.Where(x => x.User.Id == userId && x.Accessories.Id == accesId).FirstOrDefault();

            if (item == null)
            {
                item = new Cart();
                item.User = await _uManager.GetUserAsync(User);
                item.Accessories = _context.Accessores.Where(x => x.Id == accesId).FirstOrDefault();
                item.amount = 1;
                _context.Add(item);
                _context.SaveChanges();
                return Ok();
            }

            item.amount++;
            _context.Cart.Update(item);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("remove", Name = "remove")]
        public async Task<IActionResult> Remove([FromBody] int accesId)
        {
            var userId = _uManager.GetUserId(User);
            var item = _context.Cart.Where(x => x.User.Id == userId && x.Accessories.Id == accesId).FirstOrDefault();
            if (item == null)
            {
                return BadRequest();
            }
            if (item.amount <= 1) _context.Cart.Remove(item);
            else
            {
                item.amount--;
                _context.Cart.Update(item);
                
            }
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public List<Cart> GetCart()
        {
            var userId = _uManager.GetUserId(User);
            return _context.Cart.Include(x => x.Accessories).Where( x => x.User.Id == userId ).ToList();
        }
        
    }
}
