using System.ComponentModel.DataAnnotations;
namespace WeddingPlanner.Models
{
    public class RegisterViewModel : BaseEntity
    {
        [Required(ErrorMessage = "First Name is required!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First Name should be all characters!")]
        [MinLength(4, ErrorMessage = "First Name must be atleast 4 characters!")]
        public string FirstName { get; set; }
 

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
 
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string ConfirmPass { get; set; }
    }
}