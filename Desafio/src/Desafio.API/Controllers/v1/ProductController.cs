using Desafio.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

public class ProductController : DesafioControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService, IError error) : base(error)
    {
        _productService = productService;
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost]
    public async Task<ActionResult<ProductResponse>> PostProductAsync(InsertProductRequest ProductRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.InsertAsync(ProductRequest);

        return CustomResponse(result);
    }

    [HttpGet("get-by-id")]
    public async Task<ActionResult<ProductResponse>> GetProductAsync(Guid id)
    {
        var result = await _productService.GetByIdAsync(id);

        return CustomResponse(result);

    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }
    [HttpGet("get-all-salable")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllSalabeProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut]
    public async Task<ActionResult<ProductResponse>> PutProductAsync(ProductRequest ProductRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.UpdateAsync(ProductRequest);

        return CustomResponse(result);

    }
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete]
    public async Task<ActionResult<ProductResponse>> DeleteProductAsync(Guid id)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _productService.RemoveAsync(id);

        return CustomResponse(result);

    }
}
