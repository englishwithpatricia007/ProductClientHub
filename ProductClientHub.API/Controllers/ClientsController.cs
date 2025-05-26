using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseClientJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestClientJson request)
        {
            try {
                var useCase = new RegisterClientUseCase();
                var response = useCase.Execute(request);
                return Created(string.Empty, response);
            }

            catch (ProductClientHubException ex)
            {
                var errors = ex.GetErrors();
                return BadRequest(new ResponseErrorMessagesJson(errors));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson(""));
            }
  
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id)
        {
            // Use the 'id' parameter or remove it if not needed.
            return Ok($"Client with ID {id} updated successfully.");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("All clients retrieved successfully.");
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
