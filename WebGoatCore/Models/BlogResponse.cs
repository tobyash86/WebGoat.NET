using System;

namespace WebGoatCore.Models
{
    public class BlogResponse
    {
        public int Id { get; set; }
        public int BlogEntryId { get; set; }
        public DateTime ResponseDate { get; set; }
        public string Author { get; set; }
        public string Contents { get; set; }

        public virtual BlogEntry BlogEntry { get; set; }
    }
}
