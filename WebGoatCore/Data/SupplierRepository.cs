using WebGoatCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace WebGoatCore.Data
{
    public class SupplierRepository
    {
        private readonly NorthwindContext _context;

        public SupplierRepository(NorthwindContext context)
        {
            _context = context;
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.OrderBy(s => s.CompanyName).ToList();
        }
    }
}
