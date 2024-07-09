using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;
using System.IO;
using System.Linq;

namespace MyEshop.Pages.Admin
{
    public class EditModel : PageModel
    {
        private MyEshopContext _context;
        public EditModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product { get; set; }


        public void OnGet(int id)
        {
            Product = _context.Products.Include(p => p.Item)
                .Where(p => p.Id == id)
                .Select(s => new AddEditProductViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    QuantityInStock = s.Item.QuantityInStock,
                    price = s.Item.Price,
                }).FirstOrDefault();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Find the existing product and its associated item
            var product = _context.Products
                .Include(p => p.Item)
                .FirstOrDefault(p => p.Id == Product.Id);

            if (product == null)
            {
                return NotFound();
            }

            // Update product details
            product.Name = Product.Name;
            product.Description = Product.Description;

            // Update associated item details
            product.Item.Price = Product.price;
            product.Item.QuantityInStock = Product.QuantityInStock;

            _context.SaveChanges();

            // Handle picture update if needed
            if (Product.Picture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    product.Id + Path.GetExtension(Product.Picture.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
        
}
