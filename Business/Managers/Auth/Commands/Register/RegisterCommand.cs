﻿using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Managers.Auth.Commands.Register
{
    public class RegisterCommand : RegisterDto,IRequest<IDataResult<UserDto>>
    {
        
    }
}
