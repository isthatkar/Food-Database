using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;
using AutoNuoma.ViewModels;

namespace AutoNuoma.Controllers
{
    public class RestoranasController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        DarbuotojasRepository darbuotojasRepository = new DarbuotojasRepository();
        RestoranasRepository restoranasRepository = new RestoranasRepository();
        // GET: Marke
        public ActionResult Index()
        {
            //grazinamas markiu sarašas
            return View(restoranasRepository.GetRestoranai());
        }

        // GET: Marke/Create
        public ActionResult Create()
        {
            RestoranasEditViewModel marke = new RestoranasEditViewModel();
            PopulateSelections(marke);
            return View(marke);
        }

        // POST: Marke/Create
        [HttpPost]
        public ActionResult Create(RestoranasEditViewModel collection)
        {
            try
            {
                RestoranasEditViewModel tmp = restoranasRepository.GetRestoranas(collection.Pavadinimas);
                if (tmp.Pavadinimas != null)
                {
                    ModelState.AddModelError("Pavadinimas", "Toks restoranas jau yra duomenų bazėje");
                    PopulateSelections(collection);
                    return View(collection);
                }
              if (ModelState.IsValid)
                {
                    //  išsaugo nauja restorana
                   
                    restoranasRepository.addRestoranas(collection);

                    //jei yra prideta paslaugų išųsaugojo ir paslaugas
                    if (collection.Darbuotojai != null)
                    {
                      foreach (var item in collection.Darbuotojai)
                        {

                                item.Restroranas = collection.Pavadinimas;
                                darbuotojasRepository.addDarbuotojas(item);
                           

                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Marke/Edit/5
        public ActionResult Edit(string id)
        {
            RestoranasEditViewModel restoranas = restoranasRepository.GetRestoranas(id);

            PopulateSelections(restoranas);
            List<DarbuotojasEditViewModel> darbuotojai = new List<DarbuotojasEditViewModel>();
            darbuotojai.Add(new DarbuotojasEditViewModel());
            ViewBag.produktai = darbuotojai;

            return View(restoranas);
        }

        // POST: Marke/Edit/5
        [HttpPost]
        public ActionResult Edit(string pavadinimas, RestoranasEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    restoranasRepository.updateRestoranas(collection);
              
                    if (collection.Darbuotojai != null)
                    {

                        // istrina visas sutarties uzsakytas paslaugas
                        darbuotojasRepository.deleteDarbuotojai(collection.Pavadinimas);

                        //per nauja prideda visas sutarties uzsakytas paslaugas
                        foreach (var item in collection.Darbuotojai)
                        {
                            item.Restroranas = collection.Pavadinimas;
                            darbuotojasRepository.addDarbuotojas(item);
                        }
                    }
                    else
                    {
                        darbuotojasRepository.deleteDarbuotojai(collection.Pavadinimas);

                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Marke/Delete/5
        public ActionResult Delete(string id)
        {
            RestoranasEditViewModel restoranas = restoranasRepository.GetRestoranas(id);
            return View(restoranas);
        }

        // POST: Marke/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                RestoranasEditViewModel restoranas = restoranasRepository.GetRestoranas(id);
                if (restoranasRepository.getDarbuotojuCount(restoranas.Pavadinimas) == 0)
                {
                    restoranasRepository.deleteRestoranas(id);
                }
                else
                {
                    ViewBag.naudojama = "Restoranas turi darbuotojų, todėl pašalinti negalima";
                    return View(restoranas);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(RestoranasEditViewModel restoranas)
        {
            restoranas.Darbuotojai = darbuotojasRepository.getRestoranoDarbuotojai(restoranas.Pavadinimas);
        }
    }
}
