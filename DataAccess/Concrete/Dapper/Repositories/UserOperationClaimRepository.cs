using Core.DataAccess.Concrete.Dapper;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper.Repositories
{
    public  class UserOperationClaimRepository :DpBaseRepository<UserOperationClaim>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(IDbConnection dbConnection, IDbTransaction transaction) : base(dbConnection, transaction)
        {

        }
    }
}
