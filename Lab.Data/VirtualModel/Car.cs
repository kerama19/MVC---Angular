using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Data.Model
{
    [MetadataType(typeof(CarMetadata))]
    public partial class Car
    {
    }

    public class CarMetadata
    {        
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Year")]
        public Nullable<int> Year { get; set; }

        [Display(Name = "Category")]
        public string Category { get; set; }
    }
}
