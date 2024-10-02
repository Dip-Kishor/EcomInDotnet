using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
	public class CheckOutModel
	{
		[Key]
		public int Id { get; set; }
		[StringLength(60)]
		public string Country { get; set; }
		[StringLength(60)]
		public string FirstName { get; set; }
		[StringLength(60)]
		public string LastName { get; set; }
		[StringLength(60)]
		public string? CompanyName { get; set; }
		
		public string Address { get; set; }
		[EmailAddress]
		public string Email { get; set; }
		[StringLength(60)]
		public string Phone { get; set; }
		public string? Notes {  get; set; }
		[StringLength(60)]
		public string ProductName { get; set; }
		[Column(TypeName ="Decimal(20,5)")]
		public float Amount { get; set; }

	}
}
