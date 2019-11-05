using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Interfaces;

namespace Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private NorthwindContext _context;
        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }
        public Product GetProductById(int ProductId)
        {
            return _context.Products.Single(p => p.ProductId == ProductId);
        }
        public List<Product> GetTopProducts(int NumberOfProductsToReturn)
        {
            var orderDate = DateTime.Today.AddMonths(-1);
            var topProducts = _context.Orders
                   .Where(o => o.OrderDate > orderDate)
                   .Join(_context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => od)
                   .AsEnumerable()
                   .GroupBy(od => od.Product)
                   .OrderByDescending(g => g.Sum(t => t.UnitPrice * t.Quantity))
                   .Select(g => g.Key)
                   .Take(NumberOfProductsToReturn)
                   .ToList();

            if (topProducts.Count == 0)
                topProducts = _context.Products
                    .OrderByDescending(p => p.UnitPrice)
                    .Take(NumberOfProductsToReturn)
                    .ToList();
            return topProducts;
        }
        public List<Product> GetAllProducts()
        {
            return _context.Products.OrderBy(p => p.ProductId).ToList();
        }
        public List<Product> FindProducts(string ProductName)
        {
            return _context.Products.Where(p => p.ProductName.Contains(ProductName)).ToList();
        }
        public List<Product> FindNonDiscontinuedProducts(string? ProductName, int? CategoryId)
        {
            if (ProductName == null)
            {
                ProductName = "";
            }

            List<Product> products;
            if (CategoryId == null)
                products = _context.Products.Where(p => (!p.Discontinued) && p.ProductName.Contains(ProductName)).ToList();
            else
                products = _context.Products.Where(p => (!p.Discontinued) && p.ProductName.Contains(ProductName) && p.CategoryId == CategoryId).ToList();
            return products;
        }
        public void Update(Product Product)
        {
            var old = _context.Products.Single(p => p.ProductId == Product.ProductId);
            old.CategoryId = Product.CategoryId;
            old.Discontinued = Product.Discontinued;
            old.ProductName = Product.ProductName;
            old.QuantityPerUnit = Product.QuantityPerUnit;
            old.ReorderLevel = Product.ReorderLevel;
            old.SupplierId = Product.SupplierId;
            old.UnitPrice = Product.UnitPrice;
            old.UnitsInStock = Product.UnitsInStock;
            old.UnitsOnOrder = Product.UnitsOnOrder;
            _context.SaveChanges();
        }
        public void Add(Product Product)
        {
            _context.Products.Add(Product);
            _context.SaveChanges();
        }
    }
}
