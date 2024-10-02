using Microsoft.AspNetCore.Mvc;
using EcommerceDotnet.Data;
using EcommerceDotnet.Services;
using EcommerceDotnet.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using EcommerceDotnet.Web.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
namespace EcommerceDotnet.Web.Controllers
{
    
    public class ShopController : Controller
    {
        MySession _session;

        readonly IShopService _shopService;
        readonly ICategory _categoryService;

        public ShopController(IShopService shopService, MySession session, ICategory categoryService)
        {
            
            _categoryService = categoryService;
            _shopService = shopService;
            _session = session;
        }
        //
        /*        public async Task<IActionResult> Index(int categoryId)
                {
                    var items = await _shopService.GetItemsByCategoryAsync(categoryId);
                    return View(items);
                }
        */
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? categoryId,string searchString)
		{

			var categories = _categoryService.GetCategories();
			var items = categoryId.HasValue ? await _shopService.GetItemsByCategoryAsync(categoryId.Value)
											: await _shopService.GetItemsIncludingCategoryAsync();

			var viewModel = new ItemsViewModel
			{
				Categories = categories,
				Items = items,
				SelectedCategoryId = categoryId
			};

			if (!string.IsNullOrEmpty(searchString))
			{
				items = items.Where(item => item.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
			}
			return View(viewModel);
		}

        [AllowAnonymous]
        public async Task<IActionResult> ListItemsInShop(int? id)
        {
            var item = _shopService.ListItemInShop(id ?? 0);

            return View(item);
        }


        [Authorize(Policy = "All")]
        [ActionName("Cart")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "All")]
        //POST: Shop/AddToCart
        [HttpPost, ActionName("Cart")]
        public IActionResult AddToCart(int itemId)
        {


            var item = _shopService.GetItem(itemId);
            var items = _session.CartItems;
            items.Add(item);
            _session.CartItems = items;


            return View(_session.CartItems); 
        }
        [Authorize(Policy = "All")]
        [ActionName("Delete")]
        public IActionResult RemovFromCart()
        {
            return View();
        }
        [Authorize(Policy = "All")]
        [HttpPost, ActionName("Delete")]
        public IActionResult RemoveFromCart(int itemId)
        {
              
            var item = _shopService.GetItem(itemId);
            var items = _session.CartItems;
            items.Remove(item);
            _session.CartItems = items;
            return View(_session.CartItems);
        }



        [Authorize(Policy = "All")]
        public IActionResult Checkout()
		{
			var cartItems = _session.CartItems;
			var checkoutViewModel = new CheckoutViewModel
			{
				CheckOut = new CheckOutModel(),
				CartItems = cartItems
			};

			return View(checkoutViewModel);
		}
        [Authorize(Policy = "All")]
        [HttpPost]
		public IActionResult ProcessCheckout(CheckoutViewModel model)
		{
			var cartItems = _session.CartItems;
			var checkoutItems = new List<CheckOutModel>();

			foreach (var item in cartItems)
			{
				checkoutItems.Add(new CheckOutModel
				{
					Id = item.Id,
					Country = model.CheckOut.Country,
					FirstName = model.CheckOut.FirstName,
					LastName = model.CheckOut.LastName,
					CompanyName = model.CheckOut.CompanyName,
					Address = model.CheckOut.Address,
					Email = model.CheckOut.Email,
					Phone = model.CheckOut.Phone,
					Notes = model.CheckOut.Notes,
					Amount = (float)item.Price 
				});
			}

			_shopService.Checkout(checkoutItems);
			
			_session.CartItems.Clear(); // Clear cart after checkout

			return RedirectToAction("ThankYou");
		}
        [Authorize(Policy = "All")]
        public IActionResult ThankYou()
		{
			return View();
		}
        [Authorize(Policy = "AdminAndManager")]
        public IActionResult GetCheckoutItems()
        {
            var checkoutItems = _shopService.GetCheckoutItems();
            return View(checkoutItems);
        }

        public string Serialize(object? value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
