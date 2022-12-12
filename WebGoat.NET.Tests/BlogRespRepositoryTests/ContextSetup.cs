using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGoatCore.Data;
using WebGoatCore.Models;

namespace WebGoat.NET.Tests.BlogRespRepositoryTests
{
    internal static class ContextSetup
    {
        internal static Mock<NorthwindContext> CreateContext()
        {
            // create test DB
            var context = BlogRepositoryTests.ContextSetup.CreateContext();

            var blogEntryRepo = new BlogEntryRepository(context.Object);

            var entry1 = blogEntryRepo.GetBlogEntry(1);

            var r1 = new BlogResponse() { Author = "admin", Contents = "Test Content", Id = 1, ResponseDate = DateTime.Now, BlogEntry = entry1, BlogEntryId = entry1.Id };
            var r2 = new BlogResponse() { Author = "kmitnick", Contents = "KM Test Content", Id = 2, ResponseDate = DateTime.Now, BlogEntry = entry1, BlogEntryId = entry1.Id };
            var r3 = new BlogResponse() { Author = "me", Contents = "ME Test Content", Id = 3, ResponseDate = DateTime.Now, BlogEntry = entry1, BlogEntryId = entry1.Id };

            entry1.Responses.Add(r1);
            entry1.Responses.Add(r2);
            entry1.Responses.Add(r3);

            var entriesList = new List<BlogResponse> {
                r1, r2, r3
            };
            var initialBlogEntries = entriesList.AsQueryable();

            Func<BlogResponse, EntityEntry<BlogResponse>> mockEntityEntry = (BlogResponse data) =>
            {
                var internalEntityEntry = new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType(nameof(BlogResponse), typeof(BlogResponse), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false),
                    data);

                var entityEntry = new EntityEntry<BlogResponse>(internalEntityEntry);
                return entityEntry;
            };

            var mockSet = DbSetTestUtil.CreateDbSetMock(initialBlogEntries);

            mockSet.Setup(m => m.Add(It.IsAny<BlogResponse>())).Returns((BlogResponse b) => 
            {
                entriesList.Add(b);
                return mockEntityEntry(b); 
            });

            context.SetupGet(c => c.BlogResponses).Returns(mockSet.Object);

            return context;
        }

        
    }
}
