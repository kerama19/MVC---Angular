using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Data.Model
{
    [MetadataType(typeof(AddressMetadata))]
    public partial class Address
    {
    }

    public class AddressMetadata
    {
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Zip")]
        public string Zip { get; set; }
    }
}
