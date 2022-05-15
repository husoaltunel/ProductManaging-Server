using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Managers.Products.Commands.InsertPhoto
{
    public class InsertProductCommand : InsertProductDto, IRequest<IDataResult<int>>
    {

    }
}
