using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.ViewModels
{
    public class AddUserTempViewModel
    {
        public bool IsIssuerAdmin { get; set; }
        [Display(Name = "New user's name:")]
        public string NewUsername { get; set; }
        [Display(Name = "Password:")]
        public string NewPassword { get; set; }
        [Display(Name = "Email address:")]
        public string NewEmail { get; set; }
        [Display(Name = "Make this user an administrator.")]
        public bool MakeNewUserAdmin { get; set; }
        public bool CreatedUser { get; set; }
    }
}
