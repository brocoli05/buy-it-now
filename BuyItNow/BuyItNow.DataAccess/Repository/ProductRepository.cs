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
			_db.Update(product);
		}
	}
}
