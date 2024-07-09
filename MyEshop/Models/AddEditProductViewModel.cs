using Microsoft.AspNetCore.Http;

namespace MyEshop.Models
{
    public class AddEditProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public decimal price { get; set; }
        public int QuantityInStock { get; set; }
        public IFormFile Picture { get;  set; }


    }
}
