using System.ComponentModel.DataAnnotations;

namespace biletcoo.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm new password")]
        public string NewPassword { get; set; }



    }
}
