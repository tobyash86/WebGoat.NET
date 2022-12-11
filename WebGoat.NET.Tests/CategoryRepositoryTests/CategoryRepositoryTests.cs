using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebGoatCore.Data;

namespace WebGoat.NET.Tests.CategoryRepositoryTests
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        Mock<NorthwindContext> _context;

        [SetUp]
        public void Setup()
        {
            _context = ContextSetup.CreateContext();
        }

        [Test]
        public void GetAllCategoriesTest()
        {
            CategoryRepository repo = new CategoryRepository(_context.Object);
            var cats = repo.GetAllCategories();

            Assert.That(cats.Count(), Is.EqualTo(3));
        }

        [TestCase(1, "Basic")]
        [TestCase(2, "Drink")]
        [TestCase(3, "Sandwich")]
        public void GetCategoryTest(int id, string expName)
        {
            CategoryRepository repo = new CategoryRepository(_context.Object);
            var cat = repo.GetById(id);

            Assert.That(cat.CategoryName, Is.EqualTo(expName));
        }
    }
}
