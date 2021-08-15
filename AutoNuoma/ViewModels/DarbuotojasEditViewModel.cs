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
    public class DarbuotojasEditViewModel
    {
        [DisplayName("Restoranas")]
        
        public string Restroranas { get; set; }
     

        [DisplayName("Tabelio nr.")]
        [Required]

        public int Tabelio_nr { get; set; }
        [DisplayName("Vardas")]
        [Required]

        public string Vardas { get; set; }
        [DisplayName("Pavardė")]
        [Required]

        public string Pavarde { get; set; }

        [DisplayName("Pareigos")]

        public string Pareigos { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        [DisplayName("Sutarties data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Isidarbinimo_data { get; set; }

        [DisplayName("Telefono numeris")]
        public string Tel_numeris { get; set; }

        //restoranu sąrašas pasirinkimui
        public IList<SelectListItem> RestoranaiList { get; set; }
    }
}