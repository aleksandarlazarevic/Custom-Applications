using System.ComponentModel.DataAnnotations;

namespace TemplateWebShop.Models
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Not a valid email address format!")]
        [Required(ErrorMessage = "This field is mandatory!")]
        [StringLength(100, ErrorMessage = "Please choose email address length between {1} and {2} characters.", MinimumLength = 8)]
        [DataType(DataType.EmailAddress)]
        public string UserEmailId { get; set; }

        [StringLength(100, ErrorMessage = "Please choose password length between {1} and {2} characters.", MinimumLength = 8)]
        public string Password { get; set; }

        public int UserType { get; set; }

        public bool RememberMe { get; set; }
    }
}
