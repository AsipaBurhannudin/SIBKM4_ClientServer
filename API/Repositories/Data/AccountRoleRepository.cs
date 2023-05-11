using API.Context;
using API.Models;
using API.Repositories.Interface;
using System.Data;

namespace API.Repositories.Data
{
    public class AccountRoleRepository : GeneralRepository<AccountRole, int, MyContext>, IAccountRoleRepository
    {
        public AccountRoleRepository(MyContext context) : base(context)
        {

        }
        public IEnumerable<string> GetRolesByEmail(string email)
        {
            var employeeNIK = _context.Employees.FirstOrDefault(e => e.Email == email)!.NIK;
            var accountRoles = _context.AccountRoles
                                       .Where(ar => ar.AccountNIK == employeeNIK)
                                       .Join(_context.Roles,
                                             ar => ar.RoleId,
                                             r => r.Id,
                                             (ar, r) => new { ar, r })
                                       .Select(role => role.r.Name);

            return accountRoles;
        }
    }
}
