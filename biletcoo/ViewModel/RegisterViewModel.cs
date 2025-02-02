using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace biletcoo.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }

        [Required]
        [DisplayName("Are you organizer or a user")]
        public string role { get; set; }

        public string? tax { get; set; }


    }
}
