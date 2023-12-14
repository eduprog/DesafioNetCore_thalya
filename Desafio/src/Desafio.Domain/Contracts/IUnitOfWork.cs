namespace eSistem.Demo.Domain;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
