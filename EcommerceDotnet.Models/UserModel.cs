using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Foreign key for Role
        public int RoleId { get; set; }

        // Navigation property
        public virtual UserRole Role { get; set; }
    }
}
