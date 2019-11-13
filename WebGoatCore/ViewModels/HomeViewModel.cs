using WebGoatCore.Models;
using System.Collections.Generic;

namespace WebGoatCore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> TopOffers { get; set; }
    }
}