using CodingEventsDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingEventsDemo.ViewModels
{
    public class EditEventViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description too long!")]
        public string Description { get; set; }

        [EmailAddress]
        public string ContactEmail { get; set; }
        
        public EditEventViewModel() { }

        public EditEventViewModel(Event evt)
        {
            this.Id = evt.Id;
            this.Name = evt.Name;
            this.Description = evt.Description;
            this.ContactEmail = evt.ContactEmail;
        }
    }
}
