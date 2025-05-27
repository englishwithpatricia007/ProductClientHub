using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.GetAll;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.API.UseCases.Products.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Products.Register
{
    public class RegisterProductUseCase
    {
        public ResponseShortProductJson Execute(Guid clienteId, RequestProductJson request)
        {

            var dbContext = new ProductClientHubDbContext();
            Validate(request, dbContext, clienteId);

            var entity = new Product
            {
                Name = request.Name,
                Brand = request.Brand,
                Price = request.Price,
                ClientId = clienteId
            };

            dbContext.Products.Add(entity);
            dbContext.SaveChanges();

            return new ResponseShortProductJson
            {
                Id = entity.Id,
                Name = entity.Name,
            };

        }


        private void Validate(RequestProductJson request, ProductClientHubDbContext dbContext, Guid clientId)
        {
            if (!dbContext.Clients.Any(client => client.Id == clientId))
            {
                throw new NotFoundException("Cliente não encontrado");
            }

            var validator = new RequestProductValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList(); // Simplified collection initialization

                throw new ErrorOnValidationException(errors);
            }
        }
    }
}
