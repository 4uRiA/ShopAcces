using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopAcces.Data;
using ShopAcces.Models;

namespace ShopAcces.Pages
{
    public class AddAccesModel : PageModel
    {
        private readonly ShopAcces.Data.ApplicationDbContext _context;

        public AddAccesModel(ShopAcces.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Accessores Accessores { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Accessores.Add(Accessores);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
