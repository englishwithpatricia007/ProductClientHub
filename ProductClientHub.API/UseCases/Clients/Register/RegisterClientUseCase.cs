using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Register
{
    public class RegisterClientUseCase
    {
        public ResponseClientJson Execute(RequestClientJson request)
        {
            var validator = new RegisterClientValidator();
            var result = validator.Validate(request);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(error => error.ErrorMessage)
                    .ToList(); // Simplified collection initialization

                throw new ErrorOnValidationException(errors);
            }

            return new ResponseClientJson();
        }
    }
}
