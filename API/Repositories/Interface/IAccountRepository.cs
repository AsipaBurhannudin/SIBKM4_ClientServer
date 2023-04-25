using API.Models;

namespace API.Repositories.Interface
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAll();
        Account? GetById(string employeeNIK);
        int Insert(Account account);
        int Update(Account account);
        int Delete(Account account);
    }
}
