using EcommerceDotnet.Models;
using EcommerceDotnet.Services;
using IHostingEnvironmentMvc = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceDotnet.Web.Controllers
{
    [Authorize(Policy = "AdminAndManager")]
    public class CategoryController : Controller
    {
        readonly ICategory _catService;
        private readonly IHostingEnvironmentMvc _hostingEnvironment;


        public CategoryController(ICategory catService, IHostingEnvironmentMvc hostingEnvironment)
        {
            _catService = catService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            List<CategoryModel> items = _catService.GetCategories();
            return View(items);
        }
        // GET: Item/AddItem
        public IActionResult Create()
        {
            return View();
        }
        //POST: Item/Create
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> AddCategory([Bind("CId,Name")] CategoryModel catmodel)
        {
            if (ModelState.IsValid)
            {
                _catService.AddCategory(catmodel);
                return RedirectToAction(nameof(Index));
            }
            return View(catmodel);
        }
        // GET: Item/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookModel = _catService.GetCategory(id ?? 0);
            if (bookModel == null)
            {
                return NotFound();
            }

            return View(bookModel);
        }

        // POST: Item/Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            _catService.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = _catService.GetCategory(id ?? 0);
            if (itemModel == null)
            {
                return NotFound();
            }
            return View(itemModel);
        }

        //Post/Edit

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> UpdateCategory(int id, [Bind("Id,Name")] CategoryModel catmodel)
        {
            if (id != catmodel.CId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _catService.UpdateCategory(catmodel);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred while updating category {ex.Message}");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(catmodel);
        }
        // GET: Shop/GetItem
        public async Task<IActionResult> Details(int? id)
        {
            var item = _catService.GetCategory(id ?? 0);

            return View(item);
        }
    }
}
