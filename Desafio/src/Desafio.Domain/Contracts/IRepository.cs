namespace eSistem.Demo.Domain;

public interface IRepository
{
    Task<bool> Commit();
}
