using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Adresas
    {
        [DisplayName("Gatve")]
        [Required]
        public string Gatve { get; set; }
        [DisplayName("Namo numeris")]
    
        public string Namo_numeris { get; set; }
        [DisplayName("Pasto kodas")]
       
        public string Paso_kodas { get; set; }
        [DisplayName("Miestas")]
        [Required]
        public string Miestas { get; set; }
        [DisplayName("Savivaldybe")]
       
        public string Savivaldybe { get; set; }
    }
}