using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class DarbuotojasController : Controller
    {
        //Apibrežiamos saugyklos kurios naudojamos šiame valdiklyje
        // GET: Darbuotojas
        DarbuotojasRepository darbuotojasRepository = new DarbuotojasRepository();
        RestoranasRepository restoranasRepository = new RestoranasRepository();
        public ActionResult Index()
        {
            //gražinamas darbuotoju sarašo vaizdas
            return View(darbuotojasRepository.getDarbuotojai());
        }

        // GET: Darbuotojas/Create
        public ActionResult Create()
        {
            //Grazinama darbuotojo kūrimo forma
            DarbuotojasEditViewModel darbuotojas = new DarbuotojasEditViewModel();
            PopulateSelections(darbuotojas);
            return View(darbuotojas);
        }

        // POST: Darbuotojas/Create
        [HttpPost]
        public ActionResult Create(DarbuotojasEditViewModel collection)
        {
            try
            {

                DarbuotojasEditViewModel tmp = darbuotojasRepository.getDarbuotojas(collection.Tabelio_nr);
                 if (tmp.Tabelio_nr != 0)
                {
                    ModelState.AddModelError("Tabelio_nr", "Darbuotojas su tokiu tabelio numeriu jau užregistruotas");
                    PopulateSelections(collection);
                    return View(collection);
                }
                if (ModelState.IsValid)
                {
                    darbuotojasRepository.addDarbuotojas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Darbuotojas/Edit/5
        public ActionResult Edit(int id)
        {
            DarbuotojasEditViewModel darbuotojas = darbuotojasRepository.getDarbuotojas(id);
            PopulateSelections(darbuotojas);
            return View(darbuotojas);
        }

        // POST: Modelis/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DarbuotojasEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    darbuotojasRepository.updateDarbuotojas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Darbuotojas/Delete/5
        public ActionResult Delete(int id)
        {
            return View(darbuotojasRepository.getDarbuotojas(id));
        }

        // POST: Darbuotojas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

                if (!naudojama)
                {
                    darbuotojasRepository.deleteDarbuotojas(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public void PopulateSelections(DarbuotojasEditViewModel darbuotojas)
        {
            var restoranai = restoranasRepository.GetRestoranai();
            List<SelectListItem> selectListrest = new List<SelectListItem>();

            foreach (var item in restoranai)
            {
                selectListrest.Add(new SelectListItem() { Value = Convert.ToString(item.Pavadinimas), Text = item.Pavadinimas });
            }

            darbuotojas.RestoranaiList = selectListrest;
        }
    }
}
