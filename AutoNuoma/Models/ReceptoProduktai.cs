using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class ReceptoProduktai
    {
        [DisplayName("Receptas")]
        [Required]
        public int fk_Receptasid_ { get; set; }

        [DisplayName("Produktas")]
        [Required]
        public string fk_ProduktasPavadinimas { get; set; }

    [DisplayName("Kiekis")]
        [Required]
        public int Kiekis { get; set; }
        [DisplayName("Kiekio matas")]
        [Required]
        public int Kiekio_matas { get; set; }

        public int id_ { get; set; }
       
    }
}