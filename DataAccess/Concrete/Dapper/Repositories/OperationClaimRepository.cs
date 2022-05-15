﻿using Core.Entities.Concrete;
using Core.DataAccess.Concrete.Dapper;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataAccess.Concrete.Dapper.Repositories
{
    public class OperationClaimRepository : DpBaseRepository<OperationClaim>, IOperationClaimRepository
    {
        public OperationClaimRepository(IDbConnection dbConnection,IDbTransaction transaction): base(dbConnection,transaction)
        {

        }
    }
}
