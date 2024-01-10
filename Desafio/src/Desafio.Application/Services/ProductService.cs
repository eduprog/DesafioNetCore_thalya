using AutoMapper;
using Desafio.Domain;
using Microsoft.AspNetCore.Identity;

namespace Desafio.Application;

public class ProductService : ServiceBase, IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper, IError error) : base(error)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    #region Controller Methods
    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<ProductResponse>>(await _productRepository.GetAllAsync());

        if (result == null)
        {
            Notificate("No products were found.");
            return null;
        }

        return result;
    }

    public async Task<IEnumerable<ProductResponse>> GetAllSellableAsync()
    {
        var result = _mapper.Map<IEnumerable<ProductResponse>>(await _productRepository.GetAllSellableAsync());

        if (result == null)
        {
            Notificate("No products were found.");
            return null;
        }

        return result;
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            Notificate("Product was not found.");
            return null;
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> GetByShortIdAsync(string shortId)
    {
        var product = await _productRepository.GetByShortIdAsync(shortId);

        if (product == null)
        {
            Notificate("Product was not found.");
            return null;
        }

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> InsertAsync(InsertProductRequest productRequest)
    {
        var product = _mapper.Map<Product>(productRequest);

        if (!await ExecuteValidationAsync(new ProductValidator(this), product))
        {
            return null;
        }

        product.ShortId = GenerateShortId("PRODUCT");

        await _productRepository.InsertAsync(product);
        var newProduct = _mapper.Map<ProductResponse>(product);
        return newProduct;
    }

    public async Task<ProductResponse> RemoveAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            Notificate("Product was not found.");
            return null;
        }
        await _productRepository.RemoveAsync(id);

        return null;
    }

    public async Task<ProductResponse> UpdateAsync(ProductRequest productRequest)
    {
        var existingProduct = await _productRepository.GetByIdAsync(productRequest.Id);

        if (existingProduct == null)
        {
            Notificate("The product was not found.");
            return null;
        }

        existingProduct.StoredQuantity = productRequest.StoredQuantity;
        existingProduct.Price = productRequest.Price;
        existingProduct.BarCode = productRequest.BarCode;
        existingProduct.Acronym = productRequest.Acronym;
        existingProduct.Description = productRequest.Description;
        existingProduct.Id = productRequest.Id;
        existingProduct.ShortDescription = productRequest.ShortDescription;

        if (!await ExecuteValidationAsync(new ProductValidator(this), existingProduct))
        {
            return null;
        }

        await _productRepository.UpdateAsync(existingProduct);

        var productResponse = _mapper.Map<ProductResponse>(existingProduct);

        return productResponse;
    }

    public async Task<ProductResponse> UpdateEnableProductAsync(EnabledProductRequest productRequest)
    {
        var existingProduct = await _productRepository.GetByIdAsync(productRequest.Id);

        if (existingProduct == null)
        {
            Notificate("The product was not found.");
            return null;
        }

        existingProduct.Enable = productRequest.Enable;

        await _productRepository.UpdateAsync(existingProduct);

        var productResponse = _mapper.Map<ProductResponse>(existingProduct);

        return productResponse;
    }

    public async Task<ProductResponse> UpdateSellableProductAsync(SellableProductRequest productRequest)
    {
        var existingProduct = await _productRepository.GetByIdAsync(productRequest.Id);

        if (existingProduct == null)
        {
            Notificate("The product was not found.");
            return null;
        }

        existingProduct.Sellable = productRequest.Sellable;

        await _productRepository.UpdateAsync(existingProduct);

        var productResponse = _mapper.Map<ProductResponse>(existingProduct);

        return productResponse;
    }

    #endregion

    #region Validations Methods
    public async Task<bool> ExistingBarCodeAsync(string barCode)
    {
        return await _productRepository.GetByBarCodeAsync(barCode) != null;
    }

    public async Task<bool> UnitAlreadyExistsAsync(string acronym)
    {
        return await _productRepository.UnitAlreadyExistsAsync(acronym);
    }
    #endregion
}
