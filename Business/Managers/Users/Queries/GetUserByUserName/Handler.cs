using AutoMapper;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.DataAccess.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Managers.Users.Queries.GetUserByUserName
{
    public class Handler : BaseConnection, IRequestHandler<GetUserByUserNameQuery, IDataResult<UserDto>>
    {
        private IMapper _mapper;
        public Handler(IDbConnection connection,IMapper mapper)
        {
            Connection = connection;
            _mapper = mapper;
        }
        public async Task<IDataResult<UserDto>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {
                var query = await unitOfWork.DbContext.Users.GetByFilterAsync(user => user.Username == request.Username.ToLower());
                var result = query.FirstOrDefault();
                if (!ResultUtil<User>.IsDataExist(result))
                {
                    return new ErrorDataResult<UserDto>();
                   
                }

                return new SuccessDataResult<UserDto>(_mapper.Map<UserDto>(result));
            }
        }
    }
}
