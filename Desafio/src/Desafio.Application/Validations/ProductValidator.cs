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
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .MustAsync(UniqueBarCode).WithMessage("The barcode is already been used.")
            .Length(1, 13)
            .WithMessage("BarCode must have between {0} and {1} caracteres.");
        RuleFor(x => x.Acronym)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.")
            .MustAsync(UnitIsRegistered).WithMessage("The unit is not registered. It is necessary to register before using in product registration.");
        RuleFor(x => x.Description)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.ShortDescription)
            .NotEmpty().NotNull().WithMessage("The field {PropertyName} is required.");
        RuleFor(x => x.Price)
            .GreaterThan(0m).WithMessage("The price must be greater than zero.");
    }

    private async Task<bool> UnitIsRegistered(string acronym, CancellationToken token)
    {
        // Verificar se existe cadastro de unidade para a sigla utilizada
        return await _productService.UnitAlreadyExistsAsync(acronym);
    }

    private async Task<bool> UniqueBarCode(string barCode, CancellationToken token)
    {
        // Verificar se existe cadastro utilizando o mesmo código de barras
        return !await _productService.ExistingBarCodeAsync(barCode);
    }


}
