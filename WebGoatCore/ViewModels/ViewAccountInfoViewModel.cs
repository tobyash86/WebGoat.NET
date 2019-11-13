using WebGoatCore.Models;

namespace WebGoatCore.ViewModels
{
    public class ViewAccountInfoViewModel
    {
        public Customer? Customer { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
