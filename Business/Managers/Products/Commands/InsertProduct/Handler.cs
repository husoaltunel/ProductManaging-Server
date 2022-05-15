using AutoMapper;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Managers.Products.Commands.InsertPhoto
{
    public class Handler : BaseConnection, IRequestHandler<InsertProductCommand, IDataResult<int>>
    {
        private readonly IMapper _mapper;
        public Handler( IDbConnection connection,IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<int>> Handle(InsertProductCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            { 
                var product = await unitOfWork.DbContext.Products.InsertAsync(_mapper.Map<Product>(request));

                if (!ResultUtil<int>.IsDataExist(product))
                {
                    return new ErrorDataResult<int>();
                }
                return new SuccessDataResult<int>(product);
            }

            

        }
    }
}
