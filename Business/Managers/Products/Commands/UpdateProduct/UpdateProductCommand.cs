using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Managers.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : ProductDto, IRequest<IDataResult<ProductDto>>
    {

    }
}
