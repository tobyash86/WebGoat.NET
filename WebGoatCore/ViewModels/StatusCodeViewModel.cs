using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
namespace WebGoatCore.ViewModels
{
    public class StatusCodeViewModel
    {
        [Display(Name = "HTTP response code:")]
        public int Code { get; set; }
        [Display(Name = "Message:")]
        public string Message { get; set; }

        public static StatusCodeViewModel Create(ApiResponse response)
        {
            return new StatusCodeViewModel() { Code = response.StatusCode, Message = response.Message };
        }
    }
}
