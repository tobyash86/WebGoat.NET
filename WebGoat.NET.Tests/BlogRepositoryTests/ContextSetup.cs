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

namespace WebGoat.NET.Tests.BlogRepositoryTests
{
    internal static class ContextSetup
    {
        internal static Mock<NorthwindContext> CreateContext()
        {
            // create test DB
            var initialBlogEntries = new List<BlogEntry> {
            new BlogEntry() { Author = "admin", Contents = "Test Content", Id = 1, PostedDate = DateTime.Now, Responses = new List<BlogResponse>(), Title = "Test Title" }
        }.AsQueryable();

            Func<BlogEntry, EntityEntry<BlogEntry>> mockEntityEntry = (BlogEntry data) =>
            {
                var internalEntityEntry = new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType(nameof(BlogEntry), typeof(BlogEntry), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false),
                    data);

                var entityEntry = new EntityEntry<BlogEntry>(internalEntityEntry);
                return entityEntry;
            };

            var mockSet = DbSetTestUtil.CreateDbSetMock(initialBlogEntries);

            mockSet.Setup(m => m.Add(It.IsAny<BlogEntry>())).Returns(mockEntityEntry);

            var context = new Mock<NorthwindContext>();
            context.SetupGet(c => c.BlogEntries).Returns(mockSet.Object);

            return context;
        }

        
    }
}
