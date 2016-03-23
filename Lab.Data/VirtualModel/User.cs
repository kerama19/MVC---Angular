using Lab.Data.VirtualModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Data.Model
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public virtual List<TempPermissions> TempPermissions { get; set; }
    }

    public class UserMetadata
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name="UserName")]        
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
