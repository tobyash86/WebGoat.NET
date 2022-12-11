using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGoatCore.Data;
using WebGoatCore.Models;

namespace WebGoat.NET.Tests.CategoryRepositoryTests
{
    internal static class ContextSetup
    {
        internal static Mock<NorthwindContext> CreateContext()
        {
            // create test DB
            var initialCategories = new List<Category> {
            new Category() { CategoryId = 1, CategoryName = "Basic", Description = "Basic Category", 
                Products = Array.Empty<Product>() },
            new Category() { CategoryId = 2, CategoryName = "Drink", Description = "Drinks Category",
                Products = Array.Empty<Product>() },
            new Category() { CategoryId = 3, CategoryName = "Sandwich", Description = "Sandwiches Category",
                Products = Array.Empty<Product>() },
        }.AsQueryable();

            Func<Category, EntityEntry<Category>> mockEntityEntry = (Category data) =>
            {
                var internalEntityEntry = new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType(nameof(BlogEntry), typeof(BlogEntry), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false),
                    data);

                var entityEntry = new EntityEntry<Category>(internalEntityEntry);
                return entityEntry;
            };

            var mockSet = DbSetTestUtil.CreateDbSetMock(initialCategories);

            mockSet.Setup(x => x.Find(It.IsAny<object[]>()))
                .Returns((object[] x) => initialCategories.First(c => c.CategoryId == (int)x[0]));

            var context = new Mock<NorthwindContext>();
            context.SetupGet(c => c.Categories).Returns(mockSet.Object);

            return context;
        }

        
    }
}
