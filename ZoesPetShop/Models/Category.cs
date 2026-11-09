
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZoesPetShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        public ICollection<Product> Products { get; set; }
            = new List<Product>();
    }
}
