using Core.Utilities.Results.Abstract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Managers.Products.Commands.DeletePhotoById
{
    public class DeleteProductByIdCommand : IRequest<IResult>
    {
        public int Id { get;set;}
    }
}
