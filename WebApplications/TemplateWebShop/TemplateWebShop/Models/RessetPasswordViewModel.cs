using System.ComponentModel.DataAnnotations;

namespace TemplateWebShop.Models
{
    public class RessetPasswordViewModel
    {
        public string EmailId { get; set; }

        [StringLength(100, ErrorMessage = "Password has to be at least {1} and more than {2} characters long", MinimumLength = 8)]
        [Required(ErrorMessage = "This field is mandatory!")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords don't match!")]
        public string ConfirmPassword { get; set; }
    }
}
