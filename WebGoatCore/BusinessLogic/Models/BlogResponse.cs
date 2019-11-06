using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class BlogResponse
    {
        public int Id { get; set; }
        public int BlogEntryId { get; set; }
        public BlogEntry BlogEntry { get; set; }
        public DateTime ResponseDate { get; set; }
        public string Author { get; set; }
        public string Contents { get; set; }
    }
}
