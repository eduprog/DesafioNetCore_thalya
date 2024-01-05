﻿using Desafio.Domain;

namespace Desafio.Application;
public interface IUnitService
{
    Task<UnitResponse> InsertAsync(UnitRequest unitRequest);
    Task<UnitResponse> UpdateAsync(UnitRequest unitRequest);
    Task<UnitResponse> RemoveAsync(string acronym);
    Task<UnitResponse> GetByAcronymAsync(string acronym);
    Task<IEnumerable<UnitResponse>> GetAllAsync();
    Task<bool> UnitAlreadyExists(string acronym);
}