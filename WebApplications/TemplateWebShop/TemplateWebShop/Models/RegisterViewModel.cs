using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TemplateWebShop.Models
{
    public class RegisterViewModel 
    {
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [StringLength(100, ErrorMessage = "Please choose email address length between {1} and {2} characters", MinimumLength = 8)]
        [Remote("CheckEmailExists", "Account", ErrorMessage = "Email address is taken")]
        [Required(ErrorMessage = "This field is mandatory!")]
        [DataType(DataType.EmailAddress)]
        public string UserEmailId { get; set; }

        [StringLength(100, ErrorMessage = "Password has to be at least {1} and more than {2} characters long", MinimumLength = 8)]
        [Required(ErrorMessage = "This field is mandatory!")]
        public string Password { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords don't match!")]
        public string ConfirmPasword { get; set; }

        [Display(Name = "User type")]
        [Required]
        public int UserType { get; set; }

        public bool TermsAndConditions { get; set; }
    }
}
