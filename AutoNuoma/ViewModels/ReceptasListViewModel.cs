﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class ReceptasListViewModel
    {
        public int id_ { get; set; }
      
        [DisplayName("Patiekalo pavadinimas")]

        public string PatiekalasPavadinimas { get; set; }

        [DisplayName("Sudėtingumas")]

        public string Sudetingumas { get; set; }

        [DisplayName("Laikas minnutemis")]

        public int Laikas_minutemis { get; set; }
    }
}