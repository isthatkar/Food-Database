using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AutoNuoma.Models
{
    public class Produktas
    {
        [DisplayName("Pavadinimas")]
        [Required]
        public string Pavadinimas { get; set; }

        [DisplayName("Kaina eu")]
        public double Kaina_eurais { get; set; }
        [DisplayName("Baltymai")]
        public int Baltymai { get; set; }
        [DisplayName("Angliavandeniai")]
        public int Angliavandeniai { get; set; }
        [DisplayName("Riebalai")]
        public int Riebalai { get; set; }
        [DisplayName("Kalorijos")]
        public int Kalorijos { get; set; }

    }
}