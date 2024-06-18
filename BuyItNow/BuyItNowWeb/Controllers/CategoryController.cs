using BuyItNowWeb.Data;
using BuyItNowWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuyItNowWeb.Controllers
{
    public class CategoryController : Controller
    {
        // Dependency Injection (DI): less coupling
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) 
        { 
            _db = db; 
        }
        public IActionResult Index()
        {
            // retrieve category list
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create() 
        { 
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                // Custom Validation (key, error message)
                ModelState.AddModelError("name", "Display Order cannot exactly match the Name.");
            }

            if (category.Name != null && category.Name.ToLower() == "test")
            {
                // Custom Validation (key, error message)
                ModelState.AddModelError("name", "Test is an invalid value.");
            }
            // Server Side Validation
            if (ModelState.IsValid) 
            {
                _db.Categories.Add(category); // update object 
                _db.SaveChanges();  // apply the changes to the database
                return RedirectToAction("Index", "Category");  // redirect to Index
            }
            return View();
        }
            
    }
}
