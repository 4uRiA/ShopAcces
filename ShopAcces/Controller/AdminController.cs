using Microsoft.AspNetCore.Mvc;
using ShopAcces.Data;
using ShopAcces.Models;

namespace ShopAcces.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost("addnewacces", Name = "addnewacces")]
        public async Task<IActionResult> AddNewAcces(Accessores data)
        {
            _context.Accessores.Add(data);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
 