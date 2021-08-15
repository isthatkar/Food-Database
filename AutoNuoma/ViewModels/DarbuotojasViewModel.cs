using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class DarbuotojasViewModel
    {
        [DisplayName("Restoranas")]
        public string Restroranas { get; set; }

        [DisplayName("Tabelio nr.")]
    
        public int Tabelio_nr { get; set; }
        [DisplayName("Vardas")]
     
        public string Vardas { get; set; }
        [DisplayName("Pavardė")]
    
        public string Pavarde { get; set; }

        [DisplayName("Pareigos")]

        public string Pareigos { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Isidarbinimo data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Isidarbinimo_data { get; set; }

        [DisplayName("Telefono numeris")]
        public string Tel_numeris { get; set; }
    }
}