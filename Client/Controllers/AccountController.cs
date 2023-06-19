using API.Models;
using API.ViewModels;
using Client.Repositories;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountRepository repository;

        public AccountController(AccountRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Login(LoginVM login)
        {
            var result = await repository.Login(login);
            if (result != null && result.Code == 200)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var results = await repository.Get();
            var accounts = results?.Data ?? new List<Account>();

            return View(accounts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Account account)
        {
            var result = await repository.Post(account);
            if (result != null && result.Code == 200)
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await repository.Get(id);
            var account = result?.Data;

            return View(account);
        }
    }
}