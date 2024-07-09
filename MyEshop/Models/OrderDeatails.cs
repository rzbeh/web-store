using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEshop.Models
{
    public class OrderDeatails
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public Order Order { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(("ProductId"))]
        public Product Product { get; set; }    

    }
}
