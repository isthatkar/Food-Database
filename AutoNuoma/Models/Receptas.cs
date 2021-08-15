using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Receptas
    {
        public int id_ { get; set; }

        [DisplayName("Patiekalo pavadinimas")]

        public string fk_PatiekalasPavadinimas { get; set; }

        [DisplayName("Sudėtingumas")]

        public string Sudetingumas { get; set; }

        [DisplayName("Laikas minutemis")]

        public string Laikas_minutemis { get; set; }
    }
}