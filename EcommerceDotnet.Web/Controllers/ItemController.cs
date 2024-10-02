using EcommerceDotnet.Models;
using EcommerceDotnet.Services;
using IHostingEnvironmentMvc = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceDotnet.Web.Controllers
{
    [Authorize(Policy = "AdminAndManager")]
    public class ItemController : Controller
	{
		readonly IItemService _itemService;
		readonly ICategory _categoryService;
        private readonly IHostingEnvironmentMvc _hostingEnvironment;


        public ItemController(IItemService itemService, IHostingEnvironmentMvc hostingEnvironment, ICategory categoryService)
		{
			_categoryService = categoryService;
			_itemService = itemService;
			_hostingEnvironment = hostingEnvironment;
		}
		public IActionResult Index()
		{
           
                List<ItemModel> items = _itemService.GetItemsIncludingCategory(); // Ensure Category is included
                return View(items);
        }

        // GET: Item/AddItem

        public IActionResult Create()
            {
                ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CId", "Name");
                return View();
            }
        
		//POST: Item/Create
		[HttpPost, ActionName("Create")]
		public async Task<IActionResult> AddItem([Bind("Id,Name,Price,Discount,IsDiscountPct,IsPublished,DisplayInHomePage,CategoryId")] ItemModel itemModel, IFormFile ImageFile)
		{
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }
            string filePath = Path.Combine(uploads, ImageFile.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                ImageFile.CopyTo(fileStream);
                itemModel.ImageURL = "/uploads/" + ImageFile.FileName;
            }

            if (ModelState.IsValid)
			{
				_itemService.AddItem(itemModel);
				 return RedirectToAction(nameof(Index));
			}
            ViewData["CategoryId"] = new SelectList(_categoryService.GetCategories(), "CId", "Name", itemModel.CategoryId);
            return View(itemModel);
         
		}
		// GET: Item/Delete
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var bookModel = _itemService.GetItem(id ?? 0);
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

			_itemService.DeleteItem(id);
			return RedirectToAction(nameof(Index));
		}	

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemModel = _itemService.GetItem(id.Value);
            if (itemModel == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_categoryService.GetCategories(), "CId", "Name", itemModel.CategoryId);
            
            return View(itemModel);
        }

        //Post/Edit

        [HttpPost, ActionName("Edit")]
		public async Task<IActionResult> UpdateItem(int id, [Bind("Id,Name,Price,Discount,ImageURL,IsDiscountPct,IsPublished,DisplayInHomePage,CategoryId")] ItemModel itemModel)
		{
			if (id != itemModel.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_itemService.UpdateItem(itemModel);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error occurred while updating item {ex.Message}");
				}
				return RedirectToAction(nameof(Index));
			}
            ViewBag.Catagories = new SelectList(_categoryService.GetCategories(), "CId", "Name", itemModel.CategoryId);
            return View(itemModel);
		}
		// GET: Shop/GetItem
		public async Task<IActionResult> Details(int? id)
		{
			var item = _itemService.GetItem(id ?? 0);

			return View(item);
		}	
	}
}
