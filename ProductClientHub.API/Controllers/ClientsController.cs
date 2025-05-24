using Microsoft.AspNetCore.Mvc;
using ProductClientHub.API.UseCases.Clients.Register;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;

namespace ProductClientHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseClientJson), StatusCodes.Status201Created)]
        public IActionResult Register([FromBody] RequestClientJson request)
        {
            var useCase = new RegisterClientUseCase();
            var response = useCase.Execute(request);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id)
        {
            return Ok("Client updated successfully.");
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("All clients retrieved successfully.");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok("All clients retrieved successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok("Client deleted successfully.");
        }






    }
}
