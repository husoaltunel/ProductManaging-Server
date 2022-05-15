using Core.Utilities.Results.Abstract;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Managers.Products.Queries.GetPhotosByUsername
{
    public class GetProductsQuery : IRequest<IDataResult<IEnumerable<ProductDto>>>
    {
        
    }
}
