using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
	public class CheckoutViewModel
	{
		
			public CheckOutModel CheckOut { get; set; }
			public List<ItemModel> CartItems { get; set; }
		

	}
}
