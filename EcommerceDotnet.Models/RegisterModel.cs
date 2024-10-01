using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
	public class RegisterModel
	{
        public int Id { get; set; }
		[EmailAddress]
		public string Username { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password),Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
