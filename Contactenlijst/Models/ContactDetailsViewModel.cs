using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contactenlijst.Models
{
    public class ContactDetailsViewModel
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Geslacht { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Email { get; set; }
        public int TelefoonNr { get; set; }
        public string Adres { get; set; }
        public string Beschrijving { get; set; }
        public Byte[] Avatar { get; set; }
        public string Category { get; set; }
    }
}
