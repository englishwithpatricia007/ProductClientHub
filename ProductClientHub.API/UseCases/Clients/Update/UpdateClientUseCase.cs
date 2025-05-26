using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Update
{
    public class UpdateClientUseCase
    {
        public void Execute(Guid id, RequestClientJson request)
        {
            Validate(request);
            var dbContext = new ProductClientHubDbContext();
            var entity = dbContext.Clients.FirstOrDefault(c => c.Id == id);
            if(entity is null)
            {
                throw new NotFoundException("Cliente não encontrado");
            }

            entity.Email = request.Email;   
            entity.Name = request.Name;
            dbContext.Clients.Update(entity);
            dbContext.SaveChanges();

        }

        private void Validate(RequestClientJson request)
        {
            var validator = new RequestClientValidator();
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
