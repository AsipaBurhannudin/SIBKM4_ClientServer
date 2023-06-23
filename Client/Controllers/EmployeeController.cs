using API.Models;
using Client.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository repository;

        public EmployeeController(EmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var results = await repository.Get();
            var employees = results?.Data ?? new List<Employee>();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var result = await repository.Get(id);
            var employees = result.Data;

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            var result = await repository.Post(employee);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Put(employee);
                if (result != null && result.Code == 200)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result != null && result.Code == 409)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await repository.Get(id);
            var employee = new Employee();
            if (result.Data?.NIK is null)
            {
                return View(employee);
            }
            else
            {
                employee.NIK = result.Data.NIK;
                employee.FirstName = result.Data.FirstName;
            }

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await repository.Get(id);
            var employee = result?.Data;

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var result = await repository.Delete(id);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil dihapus";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 404)
            {
                ModelState.AddModelError(string.Empty, result.Message);
            }

            var employee = await repository.Get(id);
            return View("Delete", employee?.Data);
        }
    }
}
