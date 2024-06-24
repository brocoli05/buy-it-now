using BuyItNowRazor_Temp.Data;
using BuyItNowRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuyItNowRazor_Temp.Pages.Categories
{
	[BindProperties]
    public class EditModel : PageModel
    {
		private readonly ApplicationDbContext _db;
		public Category Category { get; set; }
		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}
		
		public void OnGet(int? id)
		{
			if (id != null && id != 0)
			{
				Category = _db.Categories.Find(id);
			}
		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid) // check all the validation rules
			{
				_db.Categories.Update(Category); // update object 
				_db.SaveChanges();  // apply the changes to the database
                TempData["success"] = "Category updated successfully"; // notification
                return RedirectToPage("Index");  // redirect to Index
			}

			return Page();
		}
	}
}
