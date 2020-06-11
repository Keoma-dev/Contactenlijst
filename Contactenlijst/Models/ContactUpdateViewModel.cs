using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Contactenlijst.Models
{
    public class ContactUpdateViewModel
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Geslacht { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Email { get; set; }
        public int TelefoonNr { get; set; }
        public string Adres { get; set; }
        public string Beschrijving { get; set; }
        public IFormFile Avatar { get; set; }
        public byte[] FileBytes { get; set; }
        [DisplayName("Category")]
        public string Category { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem(){Text = "Family", Value = "Family"},
            new SelectListItem(){Text = "Colleague", Value = "Colleague"},
            new SelectListItem(){Text = "Friend", Value = "Friend"},
            new SelectListItem(){Text = "Enemy", Value = "Enemy"},
        };
    }
}
