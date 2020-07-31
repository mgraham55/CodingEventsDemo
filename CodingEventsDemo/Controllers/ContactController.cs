using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingEventsDemo.Controllers
{
    public class ContactController : Controller
    {
        private EventDbContext context;

        public ContactController(EventDbContext dbContext)
        {
            context = dbContext;
        }

        // GET /Contact
        // GET /Contact/Index
        public IActionResult Index()
        {
            List<Contact> contacts = context.Contacts.ToList();
            return View(contacts);
        }

        // GET /Contact/Add
        [HttpGet]
        public IActionResult Add()
        {
            AddContactViewModel viewModel = new AddContactViewModel();
            return View(viewModel);
        }

        // POST /Contact/Add
        [HttpPost]
        public IActionResult Add(AddContactViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Contact newContact = new Contact
                {
                    Name = viewModel.FirstName + " " + viewModel.LastName,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    City = viewModel.City
                };

                context.Contacts.Add(newContact);
                context.SaveChanges();

                return Redirect("/Contact");
            }

            return View(viewModel);
        }

        // GET /Contact/View/{id}
        public IActionResult View(int id)
        {
            Contact theContact = context.Contacts.Find(id);

            ContactDetailViewModel viewModel = new ContactDetailViewModel(theContact);

            return View(viewModel);
        }
    }
}
