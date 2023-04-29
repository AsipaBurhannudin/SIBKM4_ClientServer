using API.Context;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.Identity.Client;

namespace API.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string, MyContext>, IAccountRepository
    {
        public AccountRepository(MyContext context) : base(context)
        {

        }
        public int Register(RegisterVM registerVM)
        {
            int result = 0;
            //insert to University Table
            var university = new University
            {
                Name = registerVM.UniversityName
            };
            _context.Universities.Add(university);
            result = _context.SaveChanges();

            //Sebelum insert lakukan pengecekan apakah universitas sudah ada apa belum
            university.Id = _context.Universities.FirstOrDefault(x => x.Name == registerVM.UniversityName).Id;
            //insert to Education Table
            var education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = university.Id
            };
            _context.Educations.Add(education);
            result += _context.SaveChanges();

            //insert to Employee Table
            var employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Gender = registerVM.Gender,
                BirthDate = registerVM.BirthDate,
                Email = registerVM.Email,
                HiringDate = DateTime.Now,
                PhoneNumber = registerVM.PhoneNumber,

            };
            _context.Employees.Add(employee);
            result += _context.SaveChanges();

            //insert to Account Table
            var account = new Account
            {
                EmployeeNIK = registerVM.NIK,
                Password = registerVM.Password
            };
            _context.Accounts.Add(account);
            result += _context.SaveChanges();


            //Insert to Profiling Table
            var profiling = new Profiling
            {
                EmployeeNIK = registerVM.NIK,
                EducationId = education.Id,
            };
            _context.Profilings.Add(profiling);
            result += _context.SaveChanges();

            //insert to AccountRole Table
            var accountRole = new AccountRole
            {
                AccountNIK = registerVM.NIK,
                RoleId = 2002
            };

            return result;
        }
        public bool Login(LoginVM loginVM)
        {
            // Ambil data dari database berdasarkan Email di tabel employee
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
            return true;
        }
    }
}
