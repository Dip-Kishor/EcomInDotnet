using Microsoft.AspNetCore.Mvc;
using EcommerceDotnet.Data;
namespace EcommerceDotnet.Web.Controllers
{
	public class ItemServiceController : Controller
	{
		readonly EcommerceContext _context;
		public ItemServiceController(EcommerceContext context)
		{
			_context = context;
		}
		public IActionResult GET()
		{
			return View();
		}
	}
}
