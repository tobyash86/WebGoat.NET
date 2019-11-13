using WebGoatCore.Models;

namespace WebGoatCore.Data
{
    public class BlogResponseRepository
    {
        private readonly NorthwindContext _context;

        public BlogResponseRepository(NorthwindContext context)
        {
            _context = context;
        }

        public void CreateBlogResponse(BlogResponse response)
        {
            //TODO: should put this in a try/catch
            _context.BlogResponses.Add(response);
            _context.SaveChanges();
        }
    }
}
