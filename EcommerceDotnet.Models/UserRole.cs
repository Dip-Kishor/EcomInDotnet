using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
    public class UserRole
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }

        // Navigation property (for reverse relationship)
        public virtual ICollection<UserModel> Users { get; set; }
    }
}
