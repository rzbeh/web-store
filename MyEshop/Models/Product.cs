using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Models
{
    public class Product
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ItemId { get; set; }

        public ICollection<CategoryToProduct> categoryToProducts { get; set; }
        public Item Item { get; set; }
        public List<OrderDeatails> orderDeatails { get; set; }  

    }
}
