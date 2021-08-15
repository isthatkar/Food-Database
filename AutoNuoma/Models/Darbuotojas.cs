using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Darbuotojas
    {
        [DisplayName("Restoranas")]
        public virtual Restoranas Restroranas { get; set; }
        
        [DisplayName("Tabelio nr.")]
        [MaxLength(10)]
        [Required]
        public int Tabelio_nr { get; set; }
        [DisplayName("Vardas")]
        [MaxLength(20)]
        [Required]
        public string Vardas { get; set; }
        [DisplayName("Pavardė")]
        [MaxLength(20)]
        [Required]
        public string Pavarde { get; set; }

        [DisplayName("Pareigos")]
        [MaxLength(20)]
        [Required]
        public string Pareigos { get; set; }

        [DisplayName("Isidarbinimo data")]
        [Required]
        public DateTime Isidarbinimo_data { get; set; }

        [DisplayName("Telefono numeris")]
        [Required]
        public string Tel_numeris { get; set; }
    }
}