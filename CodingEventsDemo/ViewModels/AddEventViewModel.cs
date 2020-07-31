using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CodingEventsDemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodingEventsDemo.ViewModels
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description too long!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        public List<SelectListItem> Categories { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        public int ContactId { get; set; }
        public List<SelectListItem> Contacts { get; set; }

        public AddEventViewModel(List<EventCategory> categories, List<Contact> contacts) {
            Categories = new List<SelectListItem>();

            foreach (var category in categories)
            {
                Categories.Add(
                    new SelectListItem
                    {
                        Value = category.Id.ToString(),
                        Text = category.Name
                    }
                ); ;
            }

            Contacts = new List<SelectListItem>();
            foreach (var contact in contacts)
            {
                Contacts.Add(
                    new SelectListItem
                    {
                        Value = contact.Id.ToString(),
                        Text = contact.Name
                    });
            }

        }

        public AddEventViewModel() { }

    }
}
