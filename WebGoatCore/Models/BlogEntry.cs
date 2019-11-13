using System;
using System.Collections.Generic;

namespace WebGoatCore.Models
{
    public class BlogEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostedDate { get; set; }
        public string Contents { get; set; }
        public string Author { get; set; }

        public virtual IList<BlogResponse> Responses { get; set; }
    }
}
