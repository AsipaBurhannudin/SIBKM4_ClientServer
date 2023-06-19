using API.Base;
using API.Models;
using API.Repositories.Interface;

namespace API.Controllers;

public class EmployeeController : GeneralController<IEmployeeRepository, Employee, string>
{
    public EmployeeController(IEmployeeRepository repository) : base(repository) { }
}