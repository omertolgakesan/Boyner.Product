using Boyner.Product.Application.Products.Commands.CreateProduct;
using Boyner.Product.Application.Products.Commands.DeleteProduct;
using Boyner.Product.Application.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boyner.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductsQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Response);

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Response);

            return BadRequest();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid productId, CancellationToken cancellationToken = default)
        {
            DeleteProductCommand deleteProductCommand = new DeleteProductCommand { ProductId = productId };
            var result = await _mediator.Send(deleteProductCommand, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Response);
            }
            return BadRequest();
        }
    }
}
