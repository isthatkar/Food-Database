using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Patiekalas
    {
        [DisplayName("Pavadinimas")]
        [Required]
        public string Pavadinimas { get; set; }
        [DisplayName("Kaina_eurais")]
        [Required]
        public double Kaina_eurais { get; set; }
        [DisplayName("Kategorija")]
        public string  Kategorija { get; set; }
        //Sąrašas paslaugos kainoms
        [DisplayName("Restoranas")]
        public string fk_RestoranasPavadinimas { get; set; }
    }
}