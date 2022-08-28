using Boyner.Product.Application.Categories.Commands;
using Boyner.Product.Application.Categories.Commands.DeleteCategory;
using Boyner.Product.Application.Categories.Queries.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boyner.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCategoriesQuery(), cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetCategoryQuery { CategoryId = id }, cancellationToken);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(createCategoryCommand, cancellationToken);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCategoryCommand updateCategoryCommand, CancellationToken cancellationToken = default)
        {
            updateCategoryCommand.Id = id;
            return Ok(await _mediator.Send(updateCategoryCommand, cancellationToken));
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var deleteCategoryCommand = new DeleteCategoryCommand { Id = id };
            return Ok(await _mediator.Send(deleteCategoryCommand, cancellationToken));
        }
    }
}
