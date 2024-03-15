using EcommerceDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Services
{
	public interface IItemService
	{
		List<ItemModel> GetItems();
		ItemModel GetItem(int id);
		int UpdateItem(ItemModel item);
		int DeleteItem(int Id);
		int AddItem(ItemModel item);

		bool PublishItem(int itemId, bool hideShow);
		bool ShowInHomePage(int itemId, bool hideShow);
	}
}
