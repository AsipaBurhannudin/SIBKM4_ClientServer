using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string, MyContext>, IEmployeeRepository
    {
        public EmployeeRepository(MyContext context) : base(context)
        {

        }
        public string GetFullNameByEmail(string email)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Email == email)!;
            return employee.FirstName + " " + employee.LastName;
        }

    }
}
