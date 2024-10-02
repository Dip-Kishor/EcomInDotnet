using EcommerceDotnet.Data;
using EcommerceDotnet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace EcommerceDotnet.Services
{
    public class AccountService : IAccountService
    {
        private EcommerceContext _context;
        public AccountService(EcommerceContext context)
        {
            _context = context;
        }
		/*================================================================*/
		public async Task<int> Add(UserModel user)
		{
			user.Password=GetStringSha256Hash(user.Password);
			// Add user to the database
			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return user.Id;
		}
		

		/*================================================================*/
		public async Task<int> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return user.Id;
            }
            return -1;//user may be null here
        }

		/*================================================================*/
		public async Task<UserModel> Find(int id)
        {
            return await _context.Users.FindAsync(id);
        }

		/*================================================================*/
		public async Task<List<UserModel>> List()
        {
            return await _context.Users.ToListAsync();
        }

		/*================================================================*/
		public async Task<int> Update(UserModel user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

		/*================================================================*/
		public async Task<UserModel> Login(string email, string password)
		{
            var user = _context.Users
                .Include(u => u.Role) // This includes the related UserRole entity
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
				return null; // Username not found

			// Hash the provided password for comparison
			string hashedPassword = GetStringSha256Hash(password);

			// Compare the generated hash with the stored hash in the database
			if (hashedPassword == user.Password)
			{
                
                return user;
			}
			return null; // Incorrect password
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

		/*================================================================*//*
		                                                                     * 
		private byte[] GenerateSalt()
		{
			byte[] salt = new byte[16];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}
			return salt;
		}

		*//*================================================================*//*
		private string HashPassword(string password, byte[] salt)
		{
			using (var hmac = new HMACSHA512(salt))
			{
				byte[] hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
				return Convert.ToBase64String(hashedPassword);
			}
		}*/
	}
}
