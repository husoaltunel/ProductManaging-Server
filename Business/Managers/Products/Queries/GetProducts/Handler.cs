using AutoMapper;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Managers.Products.Queries.GetPhotosByUsername
{
    public class Handler : BaseConnection, IRequestHandler<GetProductsQuery,IDataResult<IEnumerable<ProductDto>>>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public Handler(IDbConnection dbConnection,IMediator mediator,IMapper mapper)
        {
            Connection = dbConnection;
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<IDataResult<IEnumerable<ProductDto>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Products.GetAllAsync();
                var products = result.ToList();

                if (!ResultUtil<int>.IsDataExist(products.Count()))
                {
                   return  new ErrorDataResult<IEnumerable<ProductDto>>();
                }
                var productDtos = products.Select(product => _mapper.Map<ProductDto>(product)); 


                return new SuccessDataResult<IEnumerable<ProductDto>>(productDtos);


            }
        }
    }
}
