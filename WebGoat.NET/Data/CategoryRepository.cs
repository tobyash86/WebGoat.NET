using WebGoatCore.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using WebGoat.NET.Logger;

namespace WebGoatCore.Data
{
    public class CategoryRepository
    {
        private readonly NorthwindContext _context;

        public CategoryRepository(NorthwindContext context)
        {
            _context = context;
        }

        public List<Category> GetAllCategories()
        {
            DummyLogger.Log("Calling" + nameof(GetAllCategories) + "()");
            try
            {
                return _context.Categories.OrderBy(c => c.CategoryId).ToList();
            }
            catch (Exception e)
            {
                DummyLogger.Log("Exception: " + e.Message);
                DummyLogger.Log("Trace: " + e.StackTrace);
                throw;
            }
        }

        public Category? GetById(int id)
        {
            return _context.Categories.Find(id);
        }
    }
}
