using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class ProductValidator : AbstractValidator<Product>
{
    private readonly IProductService _productService;

    public ProductValidator(IProductService productService)
    {
        _productService = productService;

        RuleFor(x => x.BarCode)
            .NotEmpty().WithMessage("The field {PropertyName} is required.")
            .Must(UniqueBarCode).WithMessage("It's not possible to remove a unit that is being used in a product.")
            .Length(2, 4)
            .WithMessage("The field {PropertyName} must have between {MinLength} and {MaxLength} caracters.");

    }

    private bool UniqueBarCode(string barCode)
    {
        // Verificar se existe cadastro utilizando o mesmo código de barras
        return !_productService.ExistingBarCode(barCode);

    }
}
