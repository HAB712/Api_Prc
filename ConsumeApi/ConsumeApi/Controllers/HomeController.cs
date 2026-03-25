using System.Diagnostics;
using ConsumeApi.Models;
using ConsumeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreApiService _api;

        public HomeController(StoreApiService api)
        {
            _api = api;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _api.GetCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDetail(int id)
        {
            var check = await _api.GetCategoryAsync(id);
            return View(check);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO ct)
        {
            if (!ModelState.IsValid)
                return View(ct);

            await _api.CreateCategoryAsync(ct);
            return RedirectToAction(nameof(GetAllCategories));
        }


        public async Task<IActionResult> UpdateCategory(int id)
        {
            var check = await _api.GetCategoryAsync(id);
            if (check != null)
                return NotFound();
            return RedirectToAction(nameof(GetAllCategories));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO ct)
        {
            if (!ModelState.IsValid) return View(ct);

            await _api.UpdateCategoryAsync(id, ct);
            return RedirectToAction(nameof(GetAllCategories));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _api.DeleteCategoryAsync(id);
            return RedirectToAction(nameof(GetAllCategories));
        }



        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
