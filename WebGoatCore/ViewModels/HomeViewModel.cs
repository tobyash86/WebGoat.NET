using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace WebGoatCore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> TopOffers { get; set; }
    }
}
