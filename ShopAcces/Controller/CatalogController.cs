using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ShopAcces.Models;
using ShopAcces.Data;

namespace ShopAcces.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private ApplicationDbContext _context;
        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        [HttpGet]
        public List<Accessores> Get(string query = "")
        {
            return _context.Accessores.Where(s => s.Name.Contains(query)).ToList();
        }
    }
}
