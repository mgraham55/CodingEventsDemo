using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodingEventsDemo.Data;
using CodingEventsDemo.Models;
using CodingEventsDemo.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingEventsDemo.Controllers
{
    public class ContactController : Controller
    {
        private EventDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ContactController(EventDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            context = dbContext;
            webHostEnvironment = hostEnvironment;
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
                string uniqueFileName = _UploadedFile(viewModel);

                Contact newContact = new Contact
                {
                    Name = viewModel.FirstName + " " + viewModel.LastName,
                    Email = viewModel.Email,
                    PhoneNumber = viewModel.PhoneNumber,
                    City = viewModel.City,
                    ProfilePicture = uniqueFileName
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


        private string _UploadedFile(AddContactViewModel viewModel)
        {
            string uniqueFileName = null;

            if (viewModel.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + viewModel.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    viewModel.ProfileImage.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
