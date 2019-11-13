using WebGoatCore.Models;
using System.Collections.Generic;

namespace WebGoatCore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> TopProducts { get; set; }
    }
}