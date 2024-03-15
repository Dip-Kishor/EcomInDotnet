using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
	public class ItemModel
	{
		//todo: data annotation
		public int Id { get; set; }
		public string Name { get; set; }
		public float Price { get; set; }
		public float Discount { get; set; }
		public string ImageURL { get; set; }
		public bool IsDiscountPct { get; set; }
		public bool IsPublished { get; set; }
		public bool DisplayInHomePage { get; set; }
	}
}
