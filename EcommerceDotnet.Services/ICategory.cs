using EcommerceDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Services
{
    public interface ICategory
    {
        List<CategoryModel> GetCategories();
        CategoryModel GetCategory(int id);
        int UpdateCategory(CategoryModel item);
        int DeleteCategory(int Id);
        int AddCategory(CategoryModel item);
    }
}
