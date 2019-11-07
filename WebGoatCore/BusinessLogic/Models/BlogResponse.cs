using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class BlogResponse
    {
        public virtual int Id { get; set; }
        public virtual int BlogEntryId { get; set; }
        public virtual BlogEntry BlogEntry { get; set; }
        public virtual DateTime ResponseDate { get; set; }
        public virtual string Author { get; set; }
        public virtual string Contents { get; set; }
    }
}
