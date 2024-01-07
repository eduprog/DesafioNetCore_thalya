using AutoMapper;
using Desafio.Domain;

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

    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<ProductResponse>>(await _productRepository.GetAllAsync());

        return result;
    }

    public async Task<ProductResponse> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> InsertAsync(InsertProductRequest productRequest)
    {

        var product = _mapper.Map<Product>(productRequest);

        if (!ExecuteValidation(new ProductValidator(this), product))
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
        await _productRepository.RemoveAsync(id);

        return null;
    }

    public async Task<ProductResponse> UpdateAsync(ProductRequest productRequest)
    {
        var existingUnit = await _productRepository.GetByIdAsync(productRequest.Id);

        if (existingUnit != null)
        {
            existingUnit.StoredQuantity = productRequest.StoredQuantity;
            existingUnit.Price = productRequest.Price;
            existingUnit.BarCode = productRequest.BarCode;
            existingUnit.Salable = productRequest.Salable;
            existingUnit.Acronym = productRequest.Acronym;
            existingUnit.Description = productRequest.Description;
            existingUnit.Enable = productRequest.Enable;
            existingUnit.Id = productRequest.Id;
            existingUnit.ShortDescription = productRequest.ShortDescription;

            await _productRepository.UpdateAsync(existingUnit);

            var unitResponse = _mapper.Map<ProductResponse>(existingUnit);

            return unitResponse;
        }
        else
        {
            Notificate("The product was not found.");
            return null;
        }
    }
}
