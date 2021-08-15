using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoNuoma.Models;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class RestoranasEditViewModel
    {
        //[DisplayName("ID")]
        //public int id { get; set; }
        [DisplayName("Pavadinimas")]
        [Required]
        public string Pavadinimas { get; set; }
        [DisplayName("Virtuves tipas")]
        [Required]
        public string Virtuves_tipas { get; set; }
        [DisplayName("Atidarymo metai")]
        [Required]
        public int Atidarymo_metai { get; set; }
        [DisplayName("Tel. numeris")]
        [Required]
        public string Tel_numeris { get; set; }
        [DisplayName("Vietu skaicius")]
        [Required]
        public int Vietu_skaicius { get; set; }
        [DisplayName("Ivertinimas")]
        [Required]
        public int Ivertinimas { get; set; }
        [DisplayName("Miestas")]
        [Required]
        public string Miestas { get; set; }

        public virtual List<DarbuotojasEditViewModel> Darbuotojai { get; set; }
    }
}