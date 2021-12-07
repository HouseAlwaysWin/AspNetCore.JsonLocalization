using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.JsonLocalizationExample.Models
{
    public class FormModel
    {
        [Required(ErrorMessage = "Name_Required_MSG")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
