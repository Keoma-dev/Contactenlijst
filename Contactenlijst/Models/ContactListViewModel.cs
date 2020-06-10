using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contactenlijst.Models
{
    public class ContactListViewModel
    {
        public int Id { get; set; }
        public string Geslacht { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        
    }
}
