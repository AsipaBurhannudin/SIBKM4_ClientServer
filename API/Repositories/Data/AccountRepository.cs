using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.Identity.Client;

namespace API.Repositories.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly MyContext _context;
        public AccountRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().ToList();
        }

        public Account? GetById(string employeeNIK)
        {
            return _context.Set<Account>().FirstOrDefault(a => a.EmployeeNIK == employeeNIK);
        }

        public int Insert(Account account)
        {
            _context.Set<Account>().Add(account);
            return _context.SaveChanges();
        }

        public int Update(Account account)
        {
            _context.Set<Account>().Update(account);
            return _context.SaveChanges();
        }

        public int Delete(Account account)
        {
            _context.Set<Account>().Remove(account);
            return _context.SaveChanges();
        }
    }
}
