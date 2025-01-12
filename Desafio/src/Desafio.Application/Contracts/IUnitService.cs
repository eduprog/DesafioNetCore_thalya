﻿namespace Desafio.Application;
public interface IUnitService
{
    Task<UnitResponse> InsertAsync(UnitRequest unitRequest);
    Task<UnitResponse> UpdateAsync(UnitRequest unitRequest);
    Task<UnitResponse> RemoveAsync(string acronym);
    Task<UnitResponse> GetByAcronymAsync(string acronym);
    Task<UnitResponse> GetByShortIdAsync(string shortId);
    Task<IEnumerable<UnitResponse>> GetAllAsync();
    Task<bool> UnitDoesNotExistsAsync(string acronym);
    Task<bool> HasNotBeenUsedBeforeAsync(string acronym);
}
