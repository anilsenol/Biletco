using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace biletcoo.Models
{
    public class users : IdentityUser
    {
        [Display(Name ="First Name")]
        public string FirstName {  get; set; }
        
        public string LastName {  get; set; }

        public string password {  get; set; }

        public string gender {  get; set; }

        [DataType(DataType.DateTime)]
        public DateTime date {  get; set; }

        public string roles { get; set; }

        public string? tax { get; set; }

        public Event[] myEvents { get; set; }



    }
}
