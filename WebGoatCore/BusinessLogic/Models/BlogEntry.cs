using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class BlogEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PostedDate { get; set; }
        public string Contents { get; set; }
        public string Author { get; set; }
        public ICollection<BlogResponse> Responses { get; set; }
    }
}
