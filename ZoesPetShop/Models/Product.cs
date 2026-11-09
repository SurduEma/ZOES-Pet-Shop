
using System.ComponentModel.DataAnnotations;

namespace ZoesPetShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public string Description { get; set; } = "";

        [Range(0, 100000)]
        public decimal Price { get; set; }

        [Range(0, 100000)]
        public int Stock { get; set; }

        public string ImageUrl { get; set; }
            = "/images/product.svg";

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
