using System.ComponentModel.DataAnnotations;

namespace TemplateWebShop.Models
{
    public class ChangePasswordViewModel
    {
        [StringLength(100, ErrorMessage = "Please choose password length between {1} and {2} characters.", MinimumLength = 8)]
        [Required(ErrorMessage = "This field is mandatory!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [StringLength(100, ErrorMessage = "Please choose password length between {1} and {2} characters.", MinimumLength = 8)]
        [Required(ErrorMessage = "This field is mandatory!")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords don't match!")]
        public string ConfirmPassword { get; set; }
    }
}
