using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGoatCore.Data;
using WebGoatCore.Models;

namespace WebGoat.NET.Tests.BlogRespRepositoryTests
{
    [TestFixture]
    public class BlogResponsesTests
    {
        Mock<NorthwindContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = ContextSetup.CreateContext();
        }

        [Test]
        public void CreateBlogRespTest()
        {
            var blogEntryRepo = new BlogEntryRepository(_context.Object);
            var entry1 = blogEntryRepo.GetBlogEntry(1);
            
            var respRepo = new BlogResponseRepository(_context.Object);
            var resp = new BlogResponse() { Author = "admin", Contents = "Test", Id = 4, ResponseDate = DateTime.Now, BlogEntry = entry1, BlogEntryId = entry1.Id };

            respRepo.CreateBlogResponse(resp);

            Assert.That(_context.Object.BlogResponses.Count(), Is.EqualTo(4));
        }
    }
}
