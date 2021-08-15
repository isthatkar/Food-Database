using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Controllers
{
    public class AtaskaitaController : Controller
    {
        AtaskaituRepository ataskaituRepository = new AtaskaituRepository();
        // GET: Ataskaita
        // Gali būti nenurodytos datos dėl to prie kintamuju ? 
     

        public ActionResult Uzsakymai(DateTime ?nuo, DateTime? iki)
        {
            //Sukuriamas ataskaitos vaizdo objektas ir užpildoma duomenimis
            SutartisAtaskViewModel ataskaita = new SutartisAtaskViewModel();
            ataskaita.nuo = nuo == null ? null : nuo;
            ataskaita.iki = iki == null ? null : iki;
            ataskaita.sutartys = ataskaituRepository.getAtaskaitaUzsakymu(ataskaita.nuo, ataskaita.iki);
            //Suskaiciuojama bendra suma visų sutarčių
            foreach (var item in ataskaita.sutartys)
            {
                ataskaita.visoSuma += item.kaina;
            }

            return View(ataskaita);
        }
       
    }
}
