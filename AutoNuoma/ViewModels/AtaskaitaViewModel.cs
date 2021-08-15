using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class AtaskaitaViewModel
    {
        [DisplayName("Užsakymo nr.")]
        public int nr { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime data { get; set; }

        [DisplayName("Restorano pavadinimas")]
        public string pavadinimas { get; set; }
        [DisplayName("Užsakymų kaina")]
        public decimal kaina { get; set; }
        [DisplayName("Viso užs. patiekalų kiekis")]
        public int pat_kiekis { get; set; }

        [DisplayName("Skirtingų užs. patiekalų kiekis")]
        public int skirtPat { get; set; }
        public decimal bendraSuma { get; set; }
        [DisplayName("Kliento vardas ir pavarde")]
        public string VardasPavarde { get; set; }

    }
}