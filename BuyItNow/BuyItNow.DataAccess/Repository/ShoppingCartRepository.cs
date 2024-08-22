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
	public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
		// implement functions that already have in generic Repository interface

		private ApplicationDbContext _db;
		public ShoppingCartRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(ShoppingCart shoppingCart)
		{
			_db.Update(shoppingCart);
		}
	}
}
