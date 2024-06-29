using BuyItNow.DataAccess.Data;
using BuyItNow.DataAccess.Repository;
using BuyItNow.DataAccess.Repository.IRepository;
using BuyItNow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuyItNowWeb.Controllers
{
    public class CategoryController : Controller
    {
		// Dependency Injection (DI): less coupling
		//private readonly ApplicationDbContext _db
		//public CategoryController(ApplicationDbContext db)
		//{
		//	_db = db;
		//}
		private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		 public IActionResult Index()
        {
            // retrieve category list
            //List<Category> objCategoryList = _db.Categories.ToList();
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create() 
        { 
            return View(); 
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            // Client Side Validation
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
            if (ModelState.IsValid) // check all the validation rules
            {
				//_db.Categories.Add(category); // update object 
				//_db.SaveChanges();  // apply the changes to the database
				_unitOfWork.Category.Add(category);
				_unitOfWork.Save();
				TempData["success"] = "Category created successfully"; // notification
                return RedirectToAction("Index", "Category");  // redirect to Index
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            { 
                return NotFound();
            }
			// 3 different ways to link the Id
			//Category? categoryFromDb = _db.Categories.Find(id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u=>u.Id == id);
			//Category? categoryFromDb2 = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//Category? categoryFromDb3 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

			if (categoryFromDb == null) 
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // Server Side Validation
            if (ModelState.IsValid) // check all the validation rules
            {
				//_db.Categories.Update(category); // update object 
				//_db.SaveChanges();  // apply the changes to the database
				_unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully"; // notification
                return RedirectToAction("Index", "Category");  // redirect to Index
            }
            return View();
        }

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			//Category? categoryFromDb = _db.Categories.Find(id);
			Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			//Category? category = _db.Categories.Find(id);
			Category? category = _unitOfWork.Category.Get(u => u.Id == id);
			if (category == null)
            {
                return NotFound();
            }
			//_db.Categories.Remove(category);
			//_db.SaveChanges();  // apply the changes to the database
			_unitOfWork.Category.Remove(category);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully"; // notification
            return RedirectToAction("Index", "Category");  // redirect to Index
		}
	}
}
