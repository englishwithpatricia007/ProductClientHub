using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.UseCases.Clients.GetAll;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.API.UseCases.Clients.Update;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseShortClientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestClientJson request)
        {
            var useCase = new RegisterClientUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status404NotFound)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] RequestClientJson request)
        {
            var useCase = new UpdateClientUseCase();
            useCase.Execute(id, request);

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseAllClientsJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            var useCase = new GetAllClientsUseCase();
            var response = useCase.Execute();
            if (response.Clients.Count == 0) return NoContent();
            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Use the 'id' parameter or remove it if not needed.
            return Ok($"Client with ID {id} retrieved successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            // Use the 'id' parameter or remove it if not needed.
            return Ok($"Client with ID {id} deleted successfully.");
        }
    }
}
