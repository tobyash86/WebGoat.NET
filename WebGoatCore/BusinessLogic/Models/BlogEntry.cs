using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class BlogEntry
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual DateTime PostedDate { get; set; }
        public virtual string Contents { get; set; }
        public virtual string Author { get; set; }
        public virtual IList<BlogResponse> Responses { get; set; }
    }
}
