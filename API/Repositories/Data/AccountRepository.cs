using API.Context;
using API.Handlers;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;

namespace API.Repositories.Data;

public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
{
    public AccountRepository(MyContext context) : base(context) { }
    public int Register(RegisterVM registerVM)
    {
        int result = 0;

        // Insert to University Table
        var university = new University
        {
            Name = registerVM.UniversityName
        };

        // Sebelum insert lakukan pengecekan apakah universitas sudah ada atau belum
        if (_context.Universities.Any(u => u.Name.Contains(registerVM.UniversityName)))
        {
            university.Id = _context.Universities.FirstOrDefault(u => u.Name.Contains(registerVM.UniversityName))!.Id;
        }
        else
        {
            _context.Set<University>().Add(university);
            result = _context.SaveChanges();
        }

        // Insert to Education Table
        var education = new Education
        {
            Major = registerVM.Major,
            Degree = registerVM.Degree,
            GPA = registerVM.GPA,
            UniversityId = university.Id
        };
        _context.Set<Education>().Add(education);
        result += _context.SaveChanges();

        // Insert to Employee Table
        var employee = new Employee
        {
            NIK = registerVM.NIK,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            Gender = registerVM.Gender,
            BirthDate = registerVM.BirthDate,
            Email = registerVM.Email,
            HiringDate = DateTime.Now,
            PhoneNumber = registerVM.PhoneNumber
        };
        _context.Set<Employee>().Add(employee);
        result += _context.SaveChanges();

        // Insert to Account Table
        var account = new Account
        {
            EmployeeNIK = registerVM.NIK,
            Password = Hashing.HashPassword(registerVM.Password),
        };
        _context.Set<Account>().Add(account);
        result += _context.SaveChanges();

        // Insert to Profiling Table
        var profiling = new Profiling
        {
            EmployeeNIK = registerVM.NIK,
            EducationId = education.Id,
        };
        _context.Set<Profiling>().Add(profiling);
        result += _context.SaveChanges();

        // Insert to AccountRole Table
        var accountRole = new AccountRole
        {
            AccountNIK = registerVM.NIK,
            RoleId = 1 // user
        };
        _context.Set<AccountRole>().Add(accountRole);
        result += _context.SaveChanges();

        return result;
    }

    public bool Login(LoginVM loginVM)
    {
        var getEmployeeAccount = _context.Employees.Join(_context.Accounts,
                                                         e => e.NIK,
                                                         a => a.EmployeeNIK,
                                                         (e, a) => new {
                                                             Email = e.Email,
                                                             Password = a.Password
                                                         }).FirstOrDefault(e =>
                                                                               e.Email == loginVM.Email);

        if (getEmployeeAccount == null)
        {
            return false;
        }

        return Hashing.ValidatePassword(loginVM.Password, getEmployeeAccount.Password);
    }
}


/*Ambil data dari database berdasarkan Email di tabel employee
var employee = _context.Employees.FirstOrDefault(e => e.Email == loginVM.Email);
if (employee == null)
{
    return false;
}
// Gabungkan data dari tabel employee dengan tabel account berdasarkan NIK
var account = _context.Accounts.FirstOrDefault(a => a.EmployeeNIK == employee.NIK);
if (account == null)
{
    return false;
}
// Cocokkan data tersebut dengan password yang diinputkan
if (account.Password != loginVM.Password)
{
    return false;
}
// Cek apakah data valid atau tidak
return true;*/

