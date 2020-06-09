﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Contactenlijst.Models
{
    public class ContactCreateViewModel
    {
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public DateTime GeboorteDatum { get; set; }
        public string Email { get; set; }
        public int TelefoonNr { get; set; }
        public string Adres { get; set; }
        public string Beschrijving { get; set; }
    }
}
