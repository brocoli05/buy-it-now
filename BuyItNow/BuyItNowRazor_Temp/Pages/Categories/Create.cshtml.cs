using BuyItNowRazor_Temp.Data;
using BuyItNowRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuyItNowRazor_Temp.Pages.Categories
{
    [BindProperties]  // bind all properties
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //[BindProperty]  // bind and available in post handler when posting it
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost(Category category) 
        {
            _db.Categories.Add(category);  // update object 
			_db.SaveChanges();  // apply the changes to the database
            TempData["success"] = "Category created successfully"; // notification
            return RedirectToPage("Index");  // redirect to Index
		}
    }
}
