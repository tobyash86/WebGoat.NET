using System.Collections.Generic;
using System.Linq;
using Core;

namespace Infrastructure
{
    public class CategoryRepository
    {
        private NorthwindContext _context;
        public CategoryRepository(NorthwindContext context)
        {
            _context = context;
        }
        public List<Category> GetAllCategories()
        {
            return _context.Categories.OrderBy(c => c.CategoryId).ToList();
        }
        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }
    }
}
