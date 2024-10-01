using EcommerceDotnet.Data;
using EcommerceDotnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Services
{
    public class CategoryService : ICategory
    {
        readonly EcommerceContext dbContext;
        public CategoryService(EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public int AddCategory(CategoryModel cat)
        {
            dbContext.Categories.Add(cat);
            return dbContext.SaveChanges();
        }

        public int DeleteCategory(int Id)
        {
            CategoryModel cat = GetCategory(Id);
            dbContext.Categories.Remove(cat);
            return dbContext.SaveChanges();
        }

        public CategoryModel? GetCategory(int id)
        {
            return dbContext.Categories.Find(id);
        }
        public List<CategoryModel> GetCategories()
        {
            return dbContext.Categories.ToList();
        }
        public int UpdateCategory(CategoryModel cat)
        {
            dbContext.Categories.Update(cat);
            return dbContext.SaveChanges();
        }
    }
}
