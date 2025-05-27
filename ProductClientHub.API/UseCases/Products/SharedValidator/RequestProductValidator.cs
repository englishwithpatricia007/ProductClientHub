using FluentValidation;
using ProductClientHub.Communication.Requests;

namespace ProductClientHub.API.UseCases.Products.SharedValidator
{
    public class RequestProductValidator : AbstractValidator<RequestProductJson>
    {
        public RequestProductValidator()
        {
            RuleFor(product => product.Name).NotEmpty().WithMessage("Nome de produto inválido");
            RuleFor(product => product.Brand).NotEmpty().WithMessage("Marca de produto inválida");
            RuleFor(product => product.Price).GreaterThan(0).WithMessage("Preço para produto inválida");
        }
    }
}
