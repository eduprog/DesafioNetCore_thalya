﻿using Desafio.Domain;

namespace Desafio.Application;
public interface IProductService
{
    Task<ProductResponse> InsertAsync(InsertProductRequest productRequest);
    Task<ProductResponse> UpdateAsync(ProductRequest productRequest);
    Task<ProductResponse> UpdateEnableProductAsync(EnabledProductRequest productRequest);
    Task<ProductResponse> UpdateSellableProductAsync(SellableProductRequest productRequest);
    Task<ProductResponse> RemoveAsync(Guid id);
    Task<ProductResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllAsync();
    Task<IEnumerable<ProductResponse>> GetAllSellableAsync();
    Task<bool> ExistingBarCodeAsync(string barCode);
    Task<bool> UnitAlreadyExistsAsync(string acronym);
    Task<ProductResponse> GetByShortIdAsync(string shortId);
}

