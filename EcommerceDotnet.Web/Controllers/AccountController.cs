using EcommerceDotnet.Models;
using EcommerceDotnet.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;
using System.Net;

namespace EcommerceDotnet.Web.Controllers
{
	
	public class AccountController : Controller
	{
		readonly IAccountService _accountService;
		private readonly IHttpContextAccessor _httpcontextAccessor;
		public AccountController(IAccountService accountService, IHttpContextAccessor httpcontextAccessor)
		{
			_accountService = accountService;
			_httpcontextAccessor = httpcontextAccessor;
		}
        /*================================================================*/
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Index()
		{
			List<UserModel> items = await _accountService.List();
			return View(items);
		}
        /*================================================================*/
        [Authorize(Policy = "admin")]
        [HttpGet, ActionName("AddUser")]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost,ActionName("AddUser")]
		public async Task<IActionResult> AddUser(UserModel user)
		{
			if(user.RoleId== 0)
			{
				user.RoleId = 3;
                ModelState.Remove("Role");
				
            }
			if (ModelState.IsValid)
			{
				await _accountService.Add(user);
				return RedirectToAction("Index");
			}
			return View(user);
		}
        /*================================================================*/
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var user = await _accountService.Find(id ?? 0);
			if (user == null)
			{
				return NotFound();
			}

			return View(user);
		}

        [Authorize(Policy = "admin")]
        [HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{

			await _accountService.Delete(id);
			return RedirectToAction(nameof(Index));
		}

        /*================================================================*/

        [Authorize(Policy = "admin")]
        [HttpGet, ActionName("Edit")]

		public async Task<IActionResult> Update(int id)
		{
			var user = await _accountService.Find(id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

        [Authorize(Policy = "admin")]
        [HttpPost, ActionName("Edit")]
		public async Task<IActionResult> Update(int id, UserModel user)
		{
			if (ModelState.IsValid)
			{
				await _accountService.Update(user);
				return RedirectToAction("Index");
			}
			return View();
		}
		/*================================================================*/
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}


		[HttpPost,AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(string email, string password)
		{
			if (ModelState.IsValid)
			{
				var user = await _accountService.Login(email, password);

				if (user == null)
				{
					ModelState.AddModelError(string.Empty, "Invalid username or password");
					return View(); // Return login view with error
				}

				SetTokenCookie("Email", email!);
				SetTokenCookie("UserName", user.Username);
				SetTokenCookie("Role", user.Role.RoleName);

				var sessionValue = HttpContext.Session.GetString("Username");
				if (string.IsNullOrEmpty(sessionValue))
				{
					ModelState.AddModelError(string.Empty, "Session not set");
				}

				// Authentication successful, create claims


				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Role, user.Role.RoleName)

				};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


				if (user.Role.RoleName == "admin")
				{
					return RedirectToAction("Index", "Item");
				}
				else
				{
					return RedirectToAction("Index", "Home");
				} 
			}
			return RedirectToAction("Index");
		}
		private void SetTokenCookie(string key, string token)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				// Expires = DateTime.UtcNow.Add(timeSpan)
			};
			_httpcontextAccessor.HttpContext.Response.Cookies.Append(key, token, cookieOptions);
		}


		public IActionResult Logout()
		{
			HttpContext
				.SignOutAsync();
			var cookies = _httpcontextAccessor.HttpContext.Request.Cookies.Keys;
			foreach ( var cookie in cookies) {
				_httpcontextAccessor.HttpContext.Response.Cookies.Delete(cookie);
			}
			
			return RedirectToAction("Index","Home");
		}
		[HttpGet]
		public IActionResult AccessDenied(string returnUrl)
		{
			ViewBag.ReturnUrl = returnUrl;
			return View();
		}
	}
}
