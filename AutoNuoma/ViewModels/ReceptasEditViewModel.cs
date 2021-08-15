using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Models;

namespace AutoNuoma.ViewModels
{
    public class ReceptasEditViewModel
    {
        [DisplayName("Patiekalo pavadinimas")]
        [Required]
        public string fk_PatiekalasPavadinimas { get; set; }
        
        [Required]
        [DisplayName("Sudetingumas")]
        public int Sudetingumas { get; set; }
        [Required]
        [DisplayName("Laikas minutemis")]
        public int Laikas_minutemis { get; set; }
        public int id_ { get; set; }
       

        //Sąrašai skirti sugeneruoti pasirinkimams
        public IList<SelectListItem> PatiekalaiList { get; set; }
        public IList<SelectListItem> SudetingumaiList { get; set; }
        public IList<SelectListItem> Produktai { get; set; }
        public IList<SelectListItem> KiekioMatai { get; set; }


        public virtual List<ReceptoProduktai> ReceptoProduktaiList { get; set; }
        public virtual List<KiekioMatas> ProdKiekioMatai { get; set; }


    }
}