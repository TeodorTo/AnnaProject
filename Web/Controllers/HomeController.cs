using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Infrastructure;
using  Web.ViewModel;
using Infrastructure.Entities;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index(Guid? categoryId)
    {
        // Start with all products
        var productsQuery = _context.Products
            .Include(p => p.ProductCategories)
            .ThenInclude(pc => pc.Category)
            .AsQueryable();
        
        if (categoryId.HasValue)
        {
            productsQuery = productsQuery
                .Where(p => p.ProductCategories
                    .Any(pc => pc.CategoryId == categoryId.Value));
        }
        
        var categories = _context.Categories.ToList();
        
        var vm = new ProductsViewModel
        {
            Products = productsQuery.ToList(),
            Categories = categories,
            SelectedCategoryId = categoryId
        };

        return View(vm);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}