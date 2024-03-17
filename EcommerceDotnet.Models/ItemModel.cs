using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
	public class ItemModel
	{
		[Key]
		public int Id { get; set; }
		[StringLength(100)]
		public string Name { get; set; }
		[MaxLength(10)]
		public float Price { get; set; }
		[MaxLength(10)]
		public float Discount { get; set; }
		public string ImageURL { get; set; }
		public bool IsDiscountPct { get; set; }
		public bool IsPublished { get; set; }
		public bool DisplayInHomePage { get; set; }
	}
}
