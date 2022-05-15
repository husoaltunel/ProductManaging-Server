using AutoMapper;
using Core.Utilities.Hashing;
using Core.Utilities.Hashing.Abstract;
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Managers.Products.Commands.UpdateProduct
{
    public class Handler : BaseConnection, IRequestHandler<UpdateProductCommand, IDataResult<ProductDto>>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public Handler(IDbConnection connection, IMapper mapper, IMediator mediator)
        {
            Connection = connection;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<IDataResult<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var result = await unitOfWork.DbContext.Products.GetByFilterAsync(product => product.Id == request.Id);
                var product = result.FirstOrDefault();

                if (!ResultUtil<Product>.IsDataExist(product))
                {
                    return new ErrorDataResult<ProductDto>();
                }

                var updatedUser = _mapper.Map(request, product);
                var updateResult = await unitOfWork.DbContext.Products.UpdateAsync(updatedUser);

                if (!ResultUtil<int>.IsResultSuccees(updateResult))
                {
                    return new ErrorDataResult<ProductDto>();
                }
                else
                {
                    return new SuccessDataResult<ProductDto>(_mapper.Map<ProductDto>(updatedUser));
                }

            }
        }
    }
}
