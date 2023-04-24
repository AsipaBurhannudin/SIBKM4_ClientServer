using API.Models;

namespace API.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(string nik);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string nik);
    }
}
