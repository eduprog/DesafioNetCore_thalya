﻿using Desafio.Domain;

namespace Desafio.Application;

public class PersonRepository : IPersonRepository
{
    public Task<Person> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Person> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Person product)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Person product)
    {
        throw new NotImplementedException();
    }
}