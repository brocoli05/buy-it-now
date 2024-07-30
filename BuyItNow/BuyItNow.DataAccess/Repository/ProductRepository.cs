using BuyItNow.DataAccess.Data;
using BuyItNow.DataAccess.Repository.IRepository;
using BuyItNow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuyItNow.DataAccess.Repository
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		// implement functions that already have in generic Repository interface

		private ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Product product)
		{
			// update object directly
			//_db.Update(product);

			// retrieve data from the database
			var productFromDb = _db.Products.FirstOrDefault(u => u.Id == product.Id);
			if (productFromDb != null)
			{
				productFromDb.Title = product.Title;
				productFromDb.Description = product.Description;
				productFromDb.ListPrice = product.ListPrice;
				productFromDb.Price = product.Price;
				productFromDb.Price50 = product.Price50;
				productFromDb.Price100 = product.Price100;
				productFromDb.CategoryId = product.CategoryId;

				if (product.ImageUrl != null)
				{
					productFromDb.ImageUrl = product.ImageUrl;
				}
			}
		}
	}
}
