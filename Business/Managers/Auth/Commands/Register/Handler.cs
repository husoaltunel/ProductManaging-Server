using AutoMapper;
using Core.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Hashing.Abstract;
using Core.Utilities.Results;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Utilities.UnitOfWork;
using Entities.Dtos;
using MediatR;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.DataAccess.Concrete;
using Entities.Concrete;
using Core.Managers.Users.Queries.GetUserByUserName;
using System.Linq;

namespace Core.Managers.Auth.Commands.Register
{
    public class Handler : BaseConnection, IRequestHandler<RegisterCommand, IDataResult<UserDto>>
    {
        private IMapper _mapper;
        private IMediator _mediator;
        private IHashingHelper _hashingHelper;
        public Handler(IDbConnection dbConnection, IMapper mapper, IMediator mediator, IHashingHelper hashingHelper)
        {
            Connection = dbConnection;
            _mapper = mapper;
            _mediator = mediator;
            _hashingHelper = hashingHelper;
        }
        public async Task<IDataResult<UserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            using (var unitOfWork = UnitOfWorkUtil.GetUnitOfWork(Connection))
            {

                if (!request.Password.Equals(request.RePassword))
                    return new ErrorDataResult<UserDto>(message: AuthMessages.PasswordsNotEqual);

                var user = await _mediator.Send(new GetUserByUserNameQuery() { Username = request.Username });
                if (ResultUtil<UserDto>.IsDataExist(user.Data))
                {
                    return new ErrorDataResult<UserDto>(message: AuthMessages.UserExist);
                }

                unitOfWork.OpenConnection();
                unitOfWork.BeginTransaction();

                try
                {
                    _hashingHelper.GeneratePasswordHashAndSalt(request.Password);
                    var newUser = new User()
                    {
                        PasswordHash = _hashingHelper.GetPasswordHash(),
                        PasswordSalt = _hashingHelper.GetPasswordSalt()
                    };
                    newUser = _mapper.Map(request, newUser);
                    var insertedUserId = await unitOfWork.DbContext.Users.InsertAsync(newUser);
                    var role = await unitOfWork.DbContext.OperationClaims.GetByFilterAsync(oc => oc.Name == request.Role);

                    if (!ResultUtil<OperationClaim>.IsDataExist(role.ToList().FirstOrDefault()))
                    {
                        int insertedRoleId = await unitOfWork.DbContext.OperationClaims.InsertAsync(new OperationClaim() { Name = request.Role });
                        await unitOfWork.DbContext.UserOperationClaims.InsertAsync(new UserOperationClaim() { UserId = insertedUserId, OperationClaimId = insertedRoleId });
                    }
                    else
                    {
                        await unitOfWork.DbContext.UserOperationClaims.InsertAsync(new UserOperationClaim() { UserId = insertedUserId, OperationClaimId = role.ToList().FirstOrDefault().Id });
                    }



                    unitOfWork.Commit();

                    return await _mediator.Send(new GetUserByUserNameQuery() { Username = request.Username });

                }
                catch (System.Exception)
                {

                    unitOfWork.RollBack();
                    return new ErrorDataResult<UserDto>();
                }

            }

        }
    }
}
