using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Klientas
    {
        [DisplayName("Vardas")]
        public string Vardas { get; set; }
        [DisplayName("Pavardė")]
        public string Pavarde { get; set; }
        
        [DisplayName("Telefonas")]
        [Required]
        public string Tel_numeris { get; set; }
        [DisplayName("Elektroninis paštas")]
        [EmailAddress]
        public string El_pastas { get; set; }
    }
}