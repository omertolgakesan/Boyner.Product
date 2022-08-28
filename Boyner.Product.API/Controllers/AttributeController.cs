using Boyner.Product.Application.Attributes.Commands.CreateAttribute;
using Boyner.Product.Application.Attributes.Commands.DeleteAttribute;
using Boyner.Product.Application.Attributes.Commands.UpdateAttribute;
using Boyner.Product.Application.Attributes.Queries.GetAttributes;
using Boyner.Product.Application.SeedWork.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boyner.Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AttributeController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<AttributeController>
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(new GetAttributesQuery(), cancellationToken));
        }

        // GET api/<AttributeController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetAttributeQuery { Id = id };
            return Ok(await _mediator.Send(query, cancellationToken));
        }

        // POST api/<AttributeController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAttributeCommand createAttributeCommand, CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(createAttributeCommand, cancellationToken));
        }

        // PUT api/<AttributeController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateAttributeCommand updateAttributeCommand, CancellationToken cancellationToken = default)
        {
            updateAttributeCommand.Id = id;
            return Ok(await _mediator.Send(updateAttributeCommand, cancellationToken));
        }

        // DELETE api/<AttributeController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(new DeleteAttributeCommand { Id = id }, cancellationToken));
        }
    }
}
