using API.Models;
using API.ViewModels;
using Client.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class UniversityController : Controller
    {
        private readonly UniversityRepository repository;

        public UniversityController(UniversityRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var results = await repository.Get();
            var universities = results?.Data ?? new List<University>();

            return View(universities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(University university)
        {
            var result = await repository.Post(university);
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

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await repository.Get(id);
            var university = result?.Data;

            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(University university)
        {
            
                var result = await repository.Put(university.Id, university);
                if (result != null && result.Code == 200)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result != null && result.Code == 409)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await repository.Get(id);
            var university = new University();
            if (result.Data?.Id is null)
            {
                return View(university);
            }
            else
            {
                university.Id = result.Data.Id;
                university.Name = result.Data.Name;
            }

            return View(university);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await repository.Get(id);
            var university = result?.Data;

            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
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

            var university = await repository.Get(id);
            return View("Delete", university?.Data);
        }
    }
}