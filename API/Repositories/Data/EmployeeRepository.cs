using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyContext _context;
        public EmployeeRepository(MyContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }

        public Employee? GetById(string nik)
        {
            return _context.Employees.Include(e => e.NIK).Include(e => e.Account).SingleOrDefault(e => e.NIK == nik);
        }

        public int Insert(Employee employee)
        {
            _context.Set<Employee>().Add(employee);
            return _context.SaveChanges();
        }

        public int Update(Employee employee)
        {
            _context.Set<Employee>().Update(employee);
            return _context.SaveChanges();
        }
        public int Delete(string nik)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.NIK == nik);
            if (employee == null)
                return 0;
            _context.Set<Employee>().Remove(employee);
            return _context.SaveChanges();
        }
    }
}
