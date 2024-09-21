using ShopAcces.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopAcces.Models;

namespace ShopAcces.Pages
{
    public class CatalogModel : PageModel
    {

        public List<Accessores> accessores;

        private ApplicationDbContext _context;
        public CatalogModel(ApplicationDbContext context) 
        {

            _context = context;

        }
        public void OnGet()
        {
            accessores = _context.Accessores.ToList();
        }
    }
}
