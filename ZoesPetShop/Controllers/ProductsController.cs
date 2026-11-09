
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZoesPetShop.Data;
using ZoesPetShop.Models;

namespace ZoesPetShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string? search,
            int? categoryId,
            int page = 1)
        {
            const int pageSize = 12;

            IQueryable<Product> query = _context.Products
                .Include(p => p.Category)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();

                query = query.Where(p =>
                    EF.Functions.Like(
                        p.Name, "%" + search + "%"));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p =>
                    p.CategoryId == categoryId.Value);
            }

            int totalProducts = await query.CountAsync();

            int totalPages = Math.Max(
                1,
                (int)Math.Ceiling(totalProducts / 12.0)
            );

            page = Math.Clamp(page, 1, totalPages);

            var products = await query
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.Categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
