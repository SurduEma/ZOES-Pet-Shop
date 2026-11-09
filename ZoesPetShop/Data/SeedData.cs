
using Microsoft.EntityFrameworkCore;
using ZoesPetShop.Models;

namespace ZoesPetShop.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(
            ApplicationDbContext db)
        {
            // Do not add duplicate products
            if (await db.Products.AnyAsync())
            {
                Console.WriteLine("Products already exist.");
                return;
            }

            var categoryNames = new[]
            {
                "Premium & Hypoallergenic Food",
                "Specialized Nutrition & Supplements",
                "Hygiene & Specialized Care",
                "Toys & Premium Accessories"
            };

            string[][] productNames =
            {
                new[]
                {
                    "Hypoallergenic Salmon Dog Food",
                    "Limited Ingredient Lamb Food",
                    "Sensitive Skin Cat Food",
                    "Grain Free Turkey Formula",
                    "Single Protein Rabbit Food",
                    "Sensitive Digestion Puppy Food",
                    "Premium Duck Wet Food",
                    "Hypoallergenic Fish Cat Food"
                },

                new[]
                {
                    "Digestive Support Supplement",
                    "Omega 3 Skin and Coat Oil",
                    "Joint Support Chews",
                    "Probiotic Pet Supplement",
                    "Senior Pet Vitamin Complex",
                    "Sensitive Digestion Treats",
                    "Mobility Support Supplement",
                    "Premium Nutritional Paste"
                },

                new[]
                {
                    "Hypoallergenic Pet Shampoo",
                    "Sensitive Skin Conditioner",
                    "Dermatological Pet Shampoo",
                    "Gentle Ear Cleaning Solution",
                    "Paw Protection Balm",
                    "Fragrance Free Pet Wipes",
                    "Sensitive Skin Grooming Spray",
                    "Premium Grooming Brush"
                },

                new[]
                {
                    "Orthopedic Pet Bed",
                    "Interactive Puzzle Toy",
                    "Premium Adjustable Harness",
                    "Stainless Steel Feeding Bowl",
                    "Comfortable Training Leash",
                    "Interactive Cat Toy",
                    "Premium Scratching Post",
                    "Portable Pet Water Bottle"
                }
            };

            var descriptions = new[]
            {
                "Premium pet food with carefully selected ingredients for specific dietary needs.",
                "Specialized nutritional products designed to support everyday pet wellness.",
                "Gentle hygiene and care products for pets with specific grooming needs.",
                "High quality accessories designed for pet comfort and enrichment."
            };

            for (int i = 0; i < categoryNames.Length; i++)
            {
                var categoryName = categoryNames[i];

                var category = await db.Categories
                    .FirstOrDefaultAsync(c => c.Name == categoryName);

                if (category == null)
                {
                    category = new Category
                    {
                        Name = categoryName
                    };

                    db.Categories.Add(category);
                    await db.SaveChangesAsync();
                }

                for (int j = 0; j < productNames[i].Length; j++)
                {
                    var product = new Product
                    {
                        Name = productNames[i][j],
                        Description = descriptions[i],
                        Price = 49.90m + i * 20 + j * 10,
                        Stock = 10 + j,
                        ImageUrl = "/images/product.svg",
                        CategoryId = category.Id
                    };

                    db.Products.Add(product);
                }
            }

            await db.SaveChangesAsync();

            Console.WriteLine(
                $"Seed completed: {await db.Products.CountAsync()} products.");
        }
    }
}
