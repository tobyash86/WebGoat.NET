using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace WebGoatCore.ViewModels
{
    public class ViewAccountInfoViewModel
    {
        public Customer? Customer { get; set;}
        public string? ErrorMessage { get; set; }
    }
}
