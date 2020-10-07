using System.ComponentModel.DataAnnotations;

namespace TemplateWebShop.Models
{
    public class ForgotPasswordViewModel 
    {
        [EmailAddress(ErrorMessage = "Invalid email address format!")]
        [StringLength(100, ErrorMessage = "Please choose email address length between {1} and {2} characters", MinimumLength = 8)]
        [Required(ErrorMessage = "This field is mandatory!")]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
    }
}
