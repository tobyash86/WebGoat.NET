using System.Collections.Generic;

namespace WebGoatCore.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
