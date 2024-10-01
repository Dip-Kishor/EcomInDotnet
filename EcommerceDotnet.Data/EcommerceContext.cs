using EcommerceDotnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Data
{
	public class EcommerceContext : DbContext
	{
		public EcommerceContext(DbContextOptions<EcommerceContext> options)
	   : base(options)
		{
		}

		public DbSet<ItemModel> Items { get; set; }
		public DbSet<CheckOutModel> CheckoutItems { get; set; }
		public DbSet<UserModel> Users { get; set; }
		public DbSet<CategoryModel> Categories { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
            // Configure one-to-many relationship
            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId);

            modelBuilder.Entity<UserModel>().HasData(

				new UserModel
				{
					Id = 1,
					Username = "admin@gmail.com",
					Password = HashPassword("admindip")
				},
				new UserModel
				{
					Id=2,
					Username="manager@gmail.com",
					Password = HashPassword("managerdip")
				}
				);
		}

		/*public static class ApplicationDataInitialiser
		{
			public static void SeedData(UserManager<UserModel> userManager)
			{
				SeedUsers(userManager);
			}

			public static void SeedUsers(UserManager<UserModel> userManager)
			{
				if (userManager.FindByNameAsync("admin").Result == null)
				{
					var user = new UserModel
					{
						Username = "admin1@gmail.com",

						Password = HashPassword("admin")
					};

					var password = "PasswordWouldGoHere";

					var result = userManager.CreateAsync(user, password).Result;

					if (result.Succeeded)
					{
						userManager.AddToRoleAsync(user, "Administrator").Wait();
					}
				}
			}

		public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
		{
			if (!roleManager.RoleExistsAsync("Administrator").Result)
			{
				var role = new ApplicationRole
				{
					Name = "Administrator"
				};
				roleManager.CreateAsync(role).Wait();
			}
		}
	}*/
	
		public static string HashPassword(string password)
		{
			var passwordHasher = GetStringSha256Hash(password);
			return passwordHasher;
		}
		public static string GetStringSha256Hash(string text)
		{
			if (String.IsNullOrEmpty(text))
				return String.Empty;

			using (var sha = new System.Security.Cryptography.SHA256Managed())
			{
				byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
				byte[] hash = sha.ComputeHash(textData);
				return BitConverter.ToString(hash).Replace("-", String.Empty);
			}
		}
		//
	}
}

