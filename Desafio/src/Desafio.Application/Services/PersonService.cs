﻿using AutoMapper;
using Desafio.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Net.Http.Headers;

namespace Desafio.Application;

public class PersonService : ServiceBase, IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper, IError error) : base(error)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    #region Controller Methods
    public async Task<IEnumerable<PersonResponse>> GetAllAsync()
    {
        var result = _mapper.Map<IEnumerable<PersonResponse>>(await _personRepository.GetAllAsync());

        if(result == null || result.Count() == 0)
        {
            Notificate("No person was found");
            return null;    
        }

        return result;
    }
    public async Task<IEnumerable<PersonResponse>> GetAllClientAsync()
    {
        var result = _mapper.Map<IEnumerable<PersonResponse>>(await _personRepository.GetAllClientAsync());

        if (result == null || result.Count() == 0)
        {
            Notificate("No client was found");
            return null;
        }

        return result;
    }

    public async Task<PersonResponse> GetByIdAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            Notificate("No person was found.");
            return null;
        }

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<PersonResponse> GetClientByIdAsync(Guid id)
    {
        var person = await _personRepository.GetClientByIdAsync(id);

        if (person == null)
        {
            Notificate("No client was found.");
            return null;
        }

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<PersonResponse> GetByShortIdAsync(string shortId)
    {
        var person = await _personRepository.GetByShortIdAsync(shortId);

        if (person == null)
        {
            Notificate("No person was found.");
            return null;
        }

        return _mapper.Map<PersonResponse>(person);
    }

    public async Task<PersonResponse> InsertAsync(InsertPersonRequest personRequest)
    {
        
        var person = _mapper.Map<Person>(personRequest);

        if (!await ExecuteValidationAsync(new PersonValidator(this), person))
        {
            return null;
        }

        await _personRepository.InsertAsync(person);
        var newperson = _mapper.Map<PersonResponse>(person);
        return newperson;
    }

    public async Task<PersonResponse> RemoveAsync(Guid id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            Notificate("No person was found.");
            return null;
        }

        if (!await ExecuteValidationAsync(new RemovePersonValidator(this), person))
        {
            return null;
        }


        await _personRepository.RemoveAsync(id);

        return null;
    }

    public async Task<PersonResponse> UpdateAsync(PersonRequest personRequest)
    {
        var existingperson = await _personRepository.GetByIdAsync(personRequest.Id);

        if (existingperson == null)
        {
            Notificate("No person was found.");
            return null;
        }

        _mapper.Map<Person>(personRequest);

        if (!await ExecuteValidationAsync(new PersonValidator(this), existingperson))
        {
            return null;
        }
        _mapper.Map(personRequest, existingperson);
        await _personRepository.UpdateAsync(existingperson);

        var personResponse = _mapper.Map<PersonResponse>(existingperson);

        return personResponse;
    }

    public async Task<PersonResponse> UpdateCanBuyPersonAsync(CanBuyPersonRequest personRequest)
    {
        var existingperson = await _personRepository.GetByIdAsync(personRequest.Id);

        if (existingperson == null)
        {
            Notificate("No person was found.");
            return null;
        }
        _mapper.Map(personRequest, existingperson);
        await _personRepository.UpdateAsync(existingperson);

        var personResponse = _mapper.Map<PersonResponse>(existingperson);

        return personResponse;
    }

    public async Task<PersonResponse> UpdateEnablePersonAsync(EnabledPersonRequest personRequest)
    {
        var existingperson = await _personRepository.GetByIdAsync(personRequest.Id);

        if (existingperson == null)
        {
            Notificate("The person was not found.");
            return null;
        }

        _mapper.Map(personRequest, existingperson);
        await _personRepository.UpdateAsync(existingperson);

        return _mapper.Map<PersonResponse>(existingperson);
    }
    #endregion

    #region Validation Methods
    public async Task<bool> DocumentAlreadyExistsAsync(string document)
    {
        return await _personRepository.DocumentAlreadyExistsAsync(document);
    }
    public async Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode)
    {
        return await _personRepository.AlternativeCodeAlreadyExistsAsync(alternativeCode);
    }
    public async Task<bool> PersonCanBuyAsync(Guid id)
    {
        return await _personRepository.PersonCanBuyAsync(id);
    }
    #endregion
}
