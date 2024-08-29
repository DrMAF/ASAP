using System.ComponentModel.DataAnnotations;

namespace Core.ViewModels.Users
{
    public record LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
