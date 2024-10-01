using EcommerceDotnet.Models;
using EcommerceDotnet.Services;
using EcommerceDotnet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceDotnet.Web.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		readonly IShopService _shopService;

		public HomeController(ILogger<HomeController> logger, IShopService shopservice)
		{
			_shopService = shopservice;
			_logger = logger;
		}

		public IActionResult Index()
		{
			List<ItemModel> items = _shopService.ListItemsInHomePage();
			return View(items);
		}
		public async Task<IActionResult> ListItemsInShop(int? id)
		{
			var item = _shopService.ListItemInShop(id ?? 0);

			return View(item);
		}

		public IActionResult Privacy()
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
