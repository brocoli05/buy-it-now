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
	public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
		// implement functions that already have in generic Repository interface

		private ApplicationDbContext _db;
		public CompanyRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Company company)
		{
			_db.Update(company);
		}
	}
}
