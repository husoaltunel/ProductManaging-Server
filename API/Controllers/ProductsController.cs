using Core.Managers.Products.Commands.DeletePhotoById;
using Core.Managers.Products.Commands.InsertPhoto;
using Core.Managers.Products.Commands.UpdateProduct;
using Core.Managers.Products.Queries.GetPhotosByUsername;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthController _authController;
        public ProductsController(IMediator mediator, AuthController authController)
        {
            _mediator = mediator;
            _authController = authController;
        }
       
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _mediator.Send(new GetProductsQuery()));
        }

        [Authorize(Roles ="worker")]
        [HttpPost]
        public async Task<IActionResult> InsertAsync(InsertProductCommand product)
        {
            return Ok(await _mediator.Send(product));
        }

        [Authorize(Roles = "worker")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateProductCommand product)
        {
            return Ok(await _mediator.Send(product));
        }

        [Authorize(Roles = "worker")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            return Ok(await _mediator.Send(new DeleteProductByIdCommand(){ Id = id }));
        }



    }
}
