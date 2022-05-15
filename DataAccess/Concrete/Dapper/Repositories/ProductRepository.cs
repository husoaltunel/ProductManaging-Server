using Core.DataAccess.Concrete.Dapper;
using Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper.Repositories
{
    public class ProductRepository : DpBaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbConnection dbConnection, IDbTransaction transaction) : base(dbConnection, transaction)
        {

        }
        
    }
}
