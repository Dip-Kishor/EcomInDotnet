using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Models
{
    public class CategoryModel
    {
        [Key]
        public int CId { get; set; }
        public string Name { get; set; }

        // Navigation property for related Items
        public virtual ICollection<ItemModel> Items { get; set; } = new List<ItemModel>();
    }
}
