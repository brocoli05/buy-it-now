using BuyItNowRazor_Temp.Data;
using BuyItNowRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuyItNowRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext db) 
        { 
            _db = db; 
        }

        public void OnGet() // get method
        {
            CategoryList = _db.Categories.ToList();
            // No need to return View()
        }
    }
}
