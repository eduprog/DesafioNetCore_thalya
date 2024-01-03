﻿using Desafio.Domain;

namespace Desafio.Application;

public interface IUserRepository
{
    Task InsertAsync(User product);
    Task UpdateAsync(User product);
    Task RemoveAsync(int id);
    Task<User> GetByIdAsync(int id);
    Task<User> GetAllAsync();
}