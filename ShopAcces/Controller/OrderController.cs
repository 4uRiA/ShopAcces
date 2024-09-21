using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopAcces.Data;
using ShopAcces.Models;
using Microsoft.EntityFrameworkCore;

namespace ShopAcces.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _uManager;
        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> uManager)
        {
            _context = context;
            _uManager = uManager;
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var order = new Order();

            order.User = await _uManager.GetUserAsync(User);
            order.Address = "";
            order.Status = "Создан";

            _context.Order.Add(order);
            _context.SaveChanges();

            var userId = _uManager.GetUserId(User);

            List<OrderItem> items = new List<OrderItem>();

            List<Cart> itemcart = _context.Cart.Include(x => x.Accessories).Where(x => x.User.Id == userId).ToList();
            foreach (var item in itemcart)
            {
                items.Add(new OrderItem { Order = order, Accessores = item.Accessories, amount = item.amount });
            }

            _context.OrderItems.AddRange(items);
            _context.SaveChanges();

            _context.Cart.RemoveRange(_context.Cart.Where(x => x.User.Id == userId));
            _context.SaveChanges();


            return Ok();
        }


        [HttpGet]
        public List<Order> Get()
        {
            return _context.Order.Include(x => x.User).ToList();
        }

        [HttpPut]
        public void Put([FromBody] int orderid)
        {
            var item = _context.Order.Where(x => x.Id == orderid).FirstOrDefault();
            item.Status = "Оформлен";
            _context.Order.Update(item);
            _context.SaveChanges();
        }
    }
}
 