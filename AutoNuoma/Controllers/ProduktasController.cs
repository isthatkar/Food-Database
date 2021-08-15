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
    public class ProduktasController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos šiame valdiklyje
        ProduktasRepository produktasRepository = new ProduktasRepository();
        // GET: Klientas
        public ActionResult Index()
        {
            //grazinamas klientų sarašas
            return View(produktasRepository.getProducts());
        }

        // GET: Klientas/Create
        public ActionResult Create()
        {
            Produktas produktas = new Produktas();
            return View(produktas);
        }

        // POST: Klientas/Create
        [HttpPost]
        public ActionResult Create(Produktas collection)
        {
            try
            {
                // Patikrinama ar klientas su tokiu telefonu jau egzistuoja
                Produktas tmpkProduktas = produktasRepository.GetProduktas(collection.Pavadinimas);
                if (tmpkProduktas.Pavadinimas != null)
                {
                    ModelState.AddModelError("Pavadinimas", "Produktas su tokiu pavadinimu jau užregistruotas");
                    return View(collection);
                }
                //Jei nera sukuria nauja klienta
                if (ModelState.IsValid)
                {
                    produktasRepository.addProduktas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Klientas/Edit/5
        public ActionResult Edit(string id)
        {
            return View(produktasRepository.GetProduktas(id));
        }

        //  POST: Klientas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Produktas collection)
        {
            try
            {
                // Atnaujina kliento informacija
                if (ModelState.IsValid)
                {
                    produktasRepository.updateProduktas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Klientas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            return View(produktasRepository.GetProduktas(id));
        }

        //   POST: Klientas/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {

                bool naudojama = false;

                if (produktasRepository.getProduktasReceptuCount(id) > 0)
                {
                    naudojama = true;
                    ViewBag.naudojama = "Negalima pašalinti produktas yra naudojamas receptuose";
                    return View(produktasRepository.GetProduktas(id));
                }

                if (!naudojama)
                {
                    produktasRepository.deleteProduktas(id);

                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

    }
}
