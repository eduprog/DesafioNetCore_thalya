using Desafio.Domain;

namespace Desafio.Application;
public interface IPersonService
{
    Task<PersonResponse> InsertAsync(InsertPersonRequest personRequest);
    Task<PersonResponse> UpdateAsync(PersonRequest product);
    Task<PersonResponse> UpdateEnablePersonAsync(EnabledPersonRequest personRequest);
    Task<PersonResponse> UpdateCanBuyPersonAsync(CanBuyPersonRequest personRequest);
    Task<PersonResponse> RemoveAsync(Guid id);
    Task<PersonResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<PersonResponse>> GetAllAsync();
    Task<PersonResponse> GetByShortIdAsync(string shortId);
}
