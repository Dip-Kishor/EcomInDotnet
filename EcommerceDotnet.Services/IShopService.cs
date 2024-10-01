using EcommerceDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Services
{
    public interface IShopService
    {
        Task<IEnumerable<ItemModel>> GetItemsIncludingCategoryAsync();
		Task<IEnumerable<ItemModel>> GetItemsByCategoryAsync(int CategoryId);
        List<ItemModel> ListItemsInHomePage();
	
		ItemModel ListItemInShop(int id);
        ItemModel GetItem(int id);
        List<ItemModel> AddToCart(int itemid);
        List<ItemModel> RemoveFromCart(int itemId);
        void Checkout(List<CheckOutModel> itemIds);

        ///for retrieving information of purchased items
        List<CheckOutModel> GetCheckoutItems();

    }
}
