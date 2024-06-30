using BuyItNow.DataAccess.Repository.IRepository;
using BuyItNow.Models;
using BuyItNow.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BuyItNowWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id) //Update + Insert(Create)
        {
			// pick specific columns from the other tables using ViewModels
			ProductVM productVM = new()
			{
				CategoryList = _unitOfWork.Category
						.GetAll().Select(u => new SelectListItem
						{
							Text = u.Name,
							Value = u.Id.ToString()
						}),
				Product = new Product()
			};
            if(id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u=>u.Id == id);
				return View(productVM);
			}			
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            // Server Side Validation
            if (ModelState.IsValid) // check all the validation rules
            {
                _unitOfWork.Product.Add(productVM.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product created successfully"; // notification
                return RedirectToAction("Index", "Product");  // redirect to Index
            }
            else
            {
				productVM.CategoryList = _unitOfWork.Category
				        .GetAll().Select(u => new SelectListItem
				        {
					        Text = u.Name,
					        Value = u.Id.ToString()
				        });
				return View(productVM);
			}            
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? product = _unitOfWork.Product.Get(u => u.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successfully"; // notification
            return RedirectToAction("Index", "Product");  // redirect to Index
        }
    }
}
