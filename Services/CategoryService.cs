using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BlendMaster.Entities;

namespace BlendMaster.Services
{
    public class CategoryService
    {
        private readonly EnDbContext _context;

        public CategoryService(EnDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            // 获取所有Category对象，并创建List
            return _context.CategoryList.ToList();
        }
    }
}
