using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Concrete;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Managers.Products.Commands.DeletePhotoById
{
    public class Handler : BaseConnection, IRequestHandler<DeleteProductByIdCommand, IResult>
    {
        public Handler(IDbConnection connection)
        {
            Connection = connection;
        }
        public async Task<IResult> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var isDeleted = await unitOfWork.DbContext.Products.DeleteAsync(request.Id);

                if (!ResultUtil<int>.IsResultSuccees(isDeleted))
                {
                    return new ErrorResult();
                }
                else
                {
                    return new SuccessResult();
                }

            }
        }
    }
}
