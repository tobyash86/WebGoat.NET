using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

namespace WebGoat.NET.Tests
{
    [TestFixture]
    public class TopEntriesTests
    {
        Mock<NorthwindContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = ContextSetup.CreateContext();
        }

        [Test]
        public void GetTopEntriesTest()
        {
            var blogEntryRepo = new BlogEntryRepository(_context.Object);

            var entries = blogEntryRepo.GetTopBlogEntries();

            Assert.That(entries.Count, Is.EqualTo(1));
        }
    }
}
