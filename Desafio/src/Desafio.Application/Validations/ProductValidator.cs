using Desafio.Domain;
using FluentValidation;

namespace Desafio.Application;

public class ProductValidator : AbstractValidator<Product>
{
    private readonly IProductService _productService;

    public ProductValidator(IProductService productService)
    {
        _productService = productService;

        
    }

    //private bool UniqueAcronym(string acronym)
    //{
    //    // Verificar se existe cadastro desse produto

        
    //}
}
