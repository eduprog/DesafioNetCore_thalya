using Desafio.Domain;
using System.Threading.Tasks;

namespace Desafio.Application;
public interface IPersonService
{
    Task<PersonResponse> InsertAsync(InsertPersonRequest personRequest);
    Task<PersonResponse> UpdateAsync(PersonRequest person);
    Task<PersonResponse> UpdateEnablePersonAsync(EnabledPersonRequest personRequest);
    Task<PersonResponse> UpdateCanBuyPersonAsync(CanBuyPersonRequest personRequest);
    Task<PersonResponse> RemoveAsync(Guid id);
    Task<PersonResponse> GetByIdAsync(Guid id);
    Task<PersonResponse> GetClientByIdAsync(Guid id);
    Task<IEnumerable<PersonResponse>> GetAllAsync();
    Task<IEnumerable<PersonResponse>> GetAllClientAsync();
    Task<PersonResponse> GetByShortIdAsync(string shortId);
    Task<bool> AlternativeCodeAlreadyExistsAsync(string alternativeCode);
    Task<bool> DocumentAlreadyExistsAsync(string document);
    Task<bool> PersonCanBuyAsync(Guid id);
}
