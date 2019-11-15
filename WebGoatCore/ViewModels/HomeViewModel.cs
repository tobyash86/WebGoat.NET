using WebGoatCore.Models;
using System.Collections.Generic;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> TopProducts { get; set; }
    }
}