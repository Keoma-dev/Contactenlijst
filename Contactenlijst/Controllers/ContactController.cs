using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using Contactenlijst.Models;
using Contactenlijst.Database;
using Contactenlijst.Domain;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Contactenlijst.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDatabase _contactDatabase;

        public ContactController(IContactDatabase contactDatabase)
        {
            _contactDatabase = contactDatabase;
        }

        public IActionResult Create()
        {
            ContactCreateViewModel vm = new ContactCreateViewModel();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(ContactCreateViewModel contact)
        {
            byte[] file;

            if (contact.Avatar != null)
            {
                file = GetBytesFromFile(contact.Avatar);
            }
            else
            {
                file = new byte[] { };
            }

            Contact newContact = new Contact()
            {
                Naam = contact.Naam,
                Voornaam = contact.Voornaam,
                Geslacht = contact.Geslacht,
                GeboorteDatum = contact.GeboorteDatum,
                Email = contact.Email,
                TelefoonNr = contact.TelefoonNr,
                Adres = contact.Adres,
                Beschrijving = contact.Beschrijving,
                Avatar = file,
                Category = contact.Category
            };

            _contactDatabase.Insert(newContact);

            return RedirectToAction("Contactenoverzicht");
        }
        public IActionResult Delete(int id)
        {
            Contact contactFromDb = _contactDatabase.GetContact(id);

            ContactDeleteViewModel contact = new ContactDeleteViewModel()
            {
                Id = contactFromDb.Id,
                Naam = contactFromDb.Naam,
                Voornaam = contactFromDb.Voornaam
            };

            return View(contact);
        }
        [HttpPost]
        public IActionResult ConfirmDelete(ContactDeleteViewModel contact)
        {
            _contactDatabase.Delete(contact.Id);

            return RedirectToAction("Contactenoverzicht");
        }
        public IActionResult Details(int id)
        {
            Contact contactFromDb = _contactDatabase.GetContact(id);

            ContactDetailsViewModel contact = new ContactDetailsViewModel()
            {
                Geslacht = contactFromDb.Geslacht,
                Naam = contactFromDb.Naam,
                Voornaam = contactFromDb.Voornaam,
                GeboorteDatum = contactFromDb.GeboorteDatum,
                Email = contactFromDb.Email,
                TelefoonNr = contactFromDb.TelefoonNr,
                Adres = contactFromDb.Adres,
                Beschrijving = contactFromDb.Beschrijving,
                Avatar = contactFromDb.Avatar,
                Category = contactFromDb.Category
            };

            return View(contact);
        }
        public IActionResult Contactenoverzicht()
        {
            IEnumerable<Contact> contactsFromDb = _contactDatabase.GetContacts();
            List<ContactListViewModel> contacts = new List<ContactListViewModel>();

            foreach (var contact in contactsFromDb)
            {
                contacts.Add(new ContactListViewModel() { Id = contact.Id, Geslacht = contact.Geslacht, Naam = contact.Naam, Voornaam = contact.Voornaam });
            }

            return View(contacts);
        }

        public IActionResult Update(int id)
        {
            Contact contactFromDb = _contactDatabase.GetContact(id);
            ContactUpdateViewModel contact = new ContactUpdateViewModel()
            {
                Naam = contactFromDb.Naam,
                Voornaam = contactFromDb.Voornaam,
                Geslacht = contactFromDb.Geslacht,
                GeboorteDatum = contactFromDb.GeboorteDatum,
                Email = contactFromDb.Email,
                TelefoonNr = contactFromDb.TelefoonNr,
                Adres = contactFromDb.Adres,
                Beschrijving = contactFromDb.Beschrijving,
                FileBytes = contactFromDb.Avatar,
                Category = contactFromDb.Category
            };
            return View(contact);
        }
        [HttpPost]
        public IActionResult Update(int id, ContactUpdateViewModel contact)
        {
            Contact domainContact = new Contact()
            {
                Id = id,
                Naam = contact.Naam,
                Voornaam = contact.Voornaam,
                Geslacht = contact.Geslacht,
                GeboorteDatum = contact.GeboorteDatum,
                Email = contact.Email,
                TelefoonNr = contact.TelefoonNr,
                Adres = contact.Adres,
                Beschrijving = contact.Beschrijving,
                Category = contact.Category
            };
            if (contact.Avatar != null)
            {
                var bytes = GetBytesFromFile(contact.Avatar);
                domainContact.Avatar = bytes;
            }
            else
            {
                domainContact.Avatar = contact.FileBytes;
            }

            _contactDatabase.Update(id, domainContact);

            return RedirectToAction("Details", new { Id = id });
        }
        public Byte[] GetBytesFromFile(IFormFile file)
        {
            var extension = new FileInfo(file.FileName).Extension;
            if (extension == ".jpg" || extension == ".png" || extension == ".PNG")
            {
                using var memoryStream = new MemoryStream();
                file.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
            else
            {
                return new byte[] { };
            }
        }
    }

}
