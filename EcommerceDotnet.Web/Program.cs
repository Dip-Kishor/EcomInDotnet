using EcommerceDotnet.Data;
using EcommerceDotnet.Models;
using EcommerceDotnet.Services;
using EcommerceDotnet.Web.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static EcommerceDotnet.Data.EcommerceContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EcommerceContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceContext")));// ?? throw new InvalidOperationException("Connection string 'BWIJAN20WEBContext' not found.")));

/*builder.Services.AddIdentity<IdentityUser, IdentityRole>()
.AddEntityFrameworkStores<EcommerceContext>()
.AddSignInManager()
.AddRoles<IdentityRole>();*/

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MySession>();

builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategory, CategoryService>();
//builder.Services.AddTransient<ILoginService, LoginService>();
//Authentication
builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie((config) =>
	{
		config.ExpireTimeSpan = TimeSpan.FromMinutes(20);
		config.LoginPath = "/Account/Login";
	});

//Authorization
builder.Services.AddAuthorization((options) =>
{
	options.AddPolicy("admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("user", policy => policy.RequireRole("user"));
    options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseSession();

app.UseAuthorization();



app.MapControllerRoute(
name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
