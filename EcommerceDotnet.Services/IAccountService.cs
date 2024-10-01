using EcommerceDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Services
{
    public interface IAccountService
    {
        Task<List<UserModel>> List();
        Task<int> Add(UserModel user);
        Task<int> Update(UserModel user);
        Task<int> Delete(int id);
        Task<UserModel> Find(int id);
		Task<UserModel> Login(string username, string password);

	}
}
