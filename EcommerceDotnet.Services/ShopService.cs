using EcommerceDotnet.Data;
using EcommerceDotnet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceDotnet.Services
{
    public class ShopService : IShopService
    {
        private readonly EcommerceContext dbContext;

        public ShopService(EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<ItemModel> cartItems = new List<ItemModel>();

        //used for home page
        public List<ItemModel> ListItemsInHomePage()
        {
            return dbContext.Items.Where(x => x.DisplayInHomePage).ToList();
        }
        public async Task<IEnumerable<ItemModel>> GetItemsByCategoryAsync(int categoryId)
		{
			return await dbContext.Items
								   .Where(item => item.CategoryId == categoryId && item.IsPublished)
								   .Include(item => item.Category)
								   .ToListAsync();
		}

		public async Task<IEnumerable<ItemModel>> GetItemsIncludingCategoryAsync()
		{
			return await dbContext.Items
								   .Include(item => item.Category)
								   .ToListAsync();
		}

		public List<ItemModel> AddToCart(int itemId)
        {
            var item = dbContext.Items.Find(itemId);
            if (item != null)
            {
                cartItems.Add(item);
            }
            return(cartItems);
        }

        public List<ItemModel> RemoveFromCart(int itemId)
        {
            var item = dbContext.Items.Find(itemId);
            if (item != null)
            {
                cartItems.Remove(item);
            }
            return (cartItems);
        }


        public void Checkout(List<CheckOutModel> checkoutItems)
        {
            var itemscheckout = dbContext.CheckoutItems.Where(item => item.Id == item.Id).ToList();
            //dbContext.CheckoutItems.RemoveRange(itemscheckout);
            foreach (var item in checkoutItems)
            {
                item.Id = 0; // Reset Id to let the database generate it
            }

            dbContext.CheckoutItems.AddRange(checkoutItems);
            dbContext.SaveChanges();
        }

        public ItemModel ListItemInShop(int id)
        {
            return dbContext.Items.Find(id);
        }
        public ItemModel GetItem(int id)
        {
            return dbContext.Items.Find(id);
        }



        ///for checkout items
        public List<CheckOutModel> GetCheckoutItems()
        {
            return dbContext.CheckoutItems.ToList();
        }
    }
}
