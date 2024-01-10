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

    #region Get
    [HttpGet("get-by-id")]
    public async Task<ActionResult<ProductResponse>> GetProductAsync(Guid id)
    {
        ProductResponse result = await _productService.GetByIdAsync(id);

        return CustomResponse(result);
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }

    [HttpGet("get-all-sellable")]
    public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllSalabeProductsAsync()
    {
        IEnumerable<ProductResponse> result = await _productService.GetAllSellableAsync();
        if (!result.Any()) return CustomResponseList(result, "No Products were found.");

        return CustomResponseList(result);
    }

    [HttpGet("get-by-short-id")]
    public async Task<ActionResult<ProductResponse>> GetProductByShortIdAsync(string shortId)
    {
        ProductResponse result = await _productService.GetByShortIdAsync(shortId);

        return CustomResponse(result);
    }
    #endregion

    #region Post
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPost("insert-product")]
    public async Task<ActionResult<ProductResponse>> InsertProductAsync(InsertProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.InsertAsync(productRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Put
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-product-information")]
    public async Task<ActionResult<ProductResponse>> UpdateProductAsync(ProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.UpdateAsync(productRequest);

        return CustomResponse(result);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-enabled-property")]
    public async Task<ActionResult<bool>> UpdateProductEnabled(EnabledProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.UpdateEnableProductAsync(productRequest);

        return CustomResponse(result);
    }

    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpPut("update-sellable-property")]
    public async Task<ActionResult<bool>> UpdateProductSellable(SellableProductRequest productRequest)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.UpdateSellableProductAsync(productRequest);

        return CustomResponse(result);
    }
    #endregion

    #region Delete
    [Authorize(Roles = "ADMINISTRATOR, MANAGER")]
    [HttpDelete("delete-product")]
    public async Task<ActionResult<ProductResponse>> RemoveProductAsync(Guid id)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        ProductResponse result = await _productService.RemoveAsync(id);

        return CustomResponse(result);
    }
    #endregion






    
}
