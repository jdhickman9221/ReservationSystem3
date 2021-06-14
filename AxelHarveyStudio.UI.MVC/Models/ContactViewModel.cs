using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AxelHarveyStudio.UI.MVC.Models
{
    public class ContactViewModel
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "*Name is required")]
        [StringLength(50, ErrorMessage = "Must not be more than 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Email is required")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]//This give you standard validation.
        public string Email { get; set; }


        public string Subject { get; set; }


        [Required(ErrorMessage = "*Subject and Message both required")]
        [UIHint("MultilineText")]//UI hint for multi instead of single line. This means when it scaffolds, it creates the input of text area.
        public string Message { get; set; }

    }
}