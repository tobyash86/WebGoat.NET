using WebGoatCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebGoatCore.Data
{
    public class ProductRepository
    {
        private readonly NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public Product GetProductById(int productId)
        {
            return _context.Products.Single(p => p.ProductId == productId);
        }

        public List<Product> GetTopProducts(int numberOfProductsToReturn)
        {
            var orderDate = DateTime.Today.AddMonths(-1);
            var topProducts = _context.Orders
                   .Where(o => o.OrderDate > orderDate)
                   .Join(_context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => od)
                   .AsEnumerable()
                   .GroupBy(od => od.Product)
                   .OrderByDescending(g => g.Sum(t => t.UnitPrice * t.Quantity))
                   .Select(g => g.Key)
                   .Take(numberOfProductsToReturn)
                   .ToList();

            if (topProducts.Count == 0)
            {
                topProducts = _context.Products
                    .OrderByDescending(p => p.UnitPrice)
                    .Take(numberOfProductsToReturn)
                    .ToList();
            }

            return topProducts;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.OrderBy(p => p.ProductId).ToList();
        }

        public List<Product> FindProducts(string productName)
        {
            return _context.Products.Where(p => p.ProductName.Contains(productName)).ToList();
        }

        public List<Product> FindNonDiscontinuedProducts(string? productName, int? categoryId)
        {
            if (productName == null)
            {
                productName = "";
            }

            List<Product> products;
            if (categoryId == null)
            {
                products = _context.Products.Where(p => (!p.Discontinued) && p.ProductName.Contains(productName)).ToList();
            }
            else
            {
                products = _context.Products.Where(p => (!p.Discontinued) && p.ProductName.Contains(productName) && p.CategoryId == categoryId).ToList();
            }

            return products;
        }

        public Product Update(Product product)
        {
            var old = _context.Products.Find(product.ProductId);
            old.CategoryId = product.CategoryId;
            old.Discontinued = product.Discontinued;
            old.ProductName = product.ProductName;
            old.QuantityPerUnit = product.QuantityPerUnit;
            old.ReorderLevel = product.ReorderLevel;
            old.SupplierId = product.SupplierId;
            old.UnitPrice = product.UnitPrice;
            old.UnitsInStock = product.UnitsInStock;
            old.UnitsOnOrder = product.UnitsOnOrder;
            _context.SaveChanges();
            return old;
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}
