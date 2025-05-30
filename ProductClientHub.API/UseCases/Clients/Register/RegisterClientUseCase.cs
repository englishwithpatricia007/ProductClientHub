﻿using ProductClientHub.API.Entities;
using ProductClientHub.API.Infrastructure;
using ProductClientHub.API.UseCases.Clients.SharedValidator;
using ProductClientHub.Communication.Requests;
using ProductClientHub.Communication.Responses;
using ProductClientHub.Exceptions.ExceptionsBase;

namespace ProductClientHub.API.UseCases.Clients.Register
{
    public class RegisterClientUseCase
    {
        public ResponseShortClientJson Execute(RequestClientJson request)
        {
            Validate(request);

            var dbContext = new ProductClientHubDbContext();
            var entity = new Client
            {
                Email = request.Email,
                Name = request.Name,                
            };
            
            dbContext.Clients.Add(entity);
            dbContext.SaveChanges();

            return new ResponseShortClientJson{
                Id = entity.Id,
                Name = entity.Name,
            };
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
