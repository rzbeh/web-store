using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;
using System.IO;
using System.Linq;

namespace MyEshop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private MyEshopContext _context;
        public DeleteModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }


        public void OnGet(int id)
        {
            Product = _context.Products .FirstOrDefault(p => p.Id == id);
               
        }

        public IActionResult OnPost()
        {
            var product = _context.Products
               .Include(p => p.Item)
               .FirstOrDefault(p => p.Id == Product.Id);
           _context.Products.Remove(product);

            _context.SaveChanges();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                   "wwwroot",
                   "images",
                   product.Id + ".jpg");

            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return RedirectToPage("Index");
        }

    }
}
