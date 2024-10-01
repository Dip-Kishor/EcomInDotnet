using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
	public class ItemsViewModel
	{
		public IEnumerable<CategoryModel> Categories { get; set; }
		public IEnumerable<ItemModel> Items { get; set; }
		public int? SelectedCategoryId { get; set; }
		public string SearchString { get; set; }
	}
}
