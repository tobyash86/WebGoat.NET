using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using WebGoatCore.Data;
using WebGoatCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebGoat.NET.Tests;

public class Tests
{
    Mock<NorthwindContext> _context;

    [OneTimeSetUp]
    public void Setup()
    {
        // create test DB
        var initialBlogEntries = new List<BlogEntry> {
            new BlogEntry() { Author = "admin", Contents = "Test Content", Id = 1, PostedDate = DateTime.Now, Responses = Array.Empty<BlogResponse>(), Title = "Test Title" }
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

        var mockSet = new Mock<DbSet<BlogEntry>>();
        mockSet.As<IDbAsyncEnumerable<BlogEntry>>()
            .Setup(m => m.GetAsyncEnumerator())
        .Returns(new TestDbAsyncEnumerator<BlogEntry>(initialBlogEntries.GetEnumerator()));
        
        mockSet.Setup(m => m.Add(It.IsAny<BlogEntry>())).Returns(mockEntityEntry);

        mockSet.As<IQueryable<BlogEntry>>()
            .SetupGet(m => m.Provider).Returns(initialBlogEntries.Provider);

        _context = new Mock<NorthwindContext>();
        _context.SetupGet(c => c.BlogEntries).Returns(mockSet.Object);
    }

    [Test]
    public void TestEntryCreation()
    {
        var blogEntryRepo = new BlogEntryRepository(_context.Object);

        var entry = blogEntryRepo.CreateBlogEntry("NEW ENTRY", "NEW ENTRY CONTENT", "me");

        Assert.That(entry.Author, Is.EqualTo("me"));
    }
}