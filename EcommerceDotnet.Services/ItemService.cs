using EcommerceDotnet.Data;
using EcommerceDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Services
{
	public class ItemService:IItemService
	{
		readonly EcommerceContext dbContext;
		public ItemService(EcommerceContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public int AddItem(ItemModel item)
		{
			dbContext.Items.Add(item);
			return dbContext.SaveChanges();
		}

		public int DeleteItem(int Id)
		{
			ItemModel item = GetItem(Id);
			dbContext.Items.Remove(item);
			return dbContext.SaveChanges();
		}

		public ItemModel? GetItem(int id)
		{
			return dbContext.Items.Find(id);
		}
		public List<ItemModel> GetItems()
		{
			return dbContext.Items.ToList();
		}
		public int UpdateItem(ItemModel item)
		{
			dbContext.Items.Update(item);
			return dbContext.SaveChanges();
		}

		public bool PublishItem(int itemId, bool hideShow)
		{
			var item = dbContext.Items.FirstOrDefault(i => i.Id == itemId);
			if (item != null)
			{
				item.IsPublished = hideShow;
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool ShowInHomePage(int itemId, bool hideShow)
		{
			try
			{
				var item = dbContext.Items.Find(itemId);

				if (item == null)
				{
					Console.WriteLine($"Item with ID {itemId} not found.");
					return false;
				}
				item.DisplayInHomePage = hideShow;
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error occurred while updating item visibility: {ex.Message}");
				return false;
			}
		}
	}
}
