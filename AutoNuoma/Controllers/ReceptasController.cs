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
    public class ReceptasController : Controller
    {
        //apibreziamos saugyklos kurios naudojamos siame valdiklyje
        ReceptasRepository receptaiRepository = new ReceptasRepository();
        ReceptoProduktaiRepository receptoProduktaiRepository = new ReceptoProduktaiRepository();
        PatiekalasRepository patiekalaiRepository = new PatiekalasRepository();
        ProduktasRepository produktasRepository = new ProduktasRepository();
        ReceptoSudetingumasRepository receptoSudetingumasRepository = new ReceptoSudetingumasRepository();
        KiekioMataiRepository kiekioMataiRepository = new KiekioMataiRepository();

        // GET: Sutartis
        public ActionResult Index()
        {
            return View(receptaiRepository.GetReceptai());
        }

      //  GET: Sutartis/Create
        public ActionResult Create()
        {
            ReceptasEditViewModel receptas = new ReceptasEditViewModel();
            //uzpildo pasirinkimo sąrašus
            PopulateSelections(receptas);
            //grazinama paslaugos kurimo puslapį
            return View(receptas);
        }

        // POST: Sutartis/Create
        [HttpPost]
        public ActionResult Create(ReceptasEditViewModel collection)
        {
            try
            {
                ReceptasEditViewModel tmp = receptaiRepository.getReceptas(collection.fk_PatiekalasPavadinimas);
                if (tmp.fk_PatiekalasPavadinimas != null)
                {
                    ModelState.AddModelError("fk_PatiekalasPavadinimas", "Receptas šiam patiekalui jau yra duomenų bazėje");
                    PopulateSelections(collection);
                    return View(collection);
                }
                if (ModelState.IsValid)
                {
                    //  išsaugo nauja recepta
                    int id = receptaiRepository.getReceptuCount() + 100;
                    collection.id_ = id;
                    receptaiRepository.addReceptas(collection);

                    //jei yra prideta paslaugų išųsaugojo ir paslaugas
                    if (collection.ReceptoProduktaiList != null)
                    { 

                        //kiekviena uzsakyta paslauga isaugojama duomenu bazeje
                        foreach (var item in collection.ReceptoProduktaiList)
                        {
                            item.fk_Receptasid_ = id;
                            item.id_ = receptoProduktaiRepository.getRecProdduktuCount() + 100;
                            receptoProduktaiRepository.insertUzsakytaPaslauga(item);

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

        // GET: Sutartis/Edit/5
        public ActionResult Edit(string pavadinimas)
        {
            ReceptasEditViewModel receptas = receptaiRepository.getReceptas(pavadinimas);

            PopulateSelections(receptas);
            List<ReceptoProduktai> produktai = new List<ReceptoProduktai>();
            produktai.Add(new ReceptoProduktai());
            ViewBag.produktai = produktai;           

            return View(receptas);
        }

        // POST: Sutartis/Edit/5
        [HttpPost]
        public ActionResult Edit(string pavadinimas, ReceptasEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                 
                    receptaiRepository.updateReceptas(collection);
                    //gauna recepto id
                    int id = receptaiRepository.getReceptas(collection.fk_PatiekalasPavadinimas).id_;
                    collection.id_ = id;
                    if (collection.ReceptoProduktaiList != null)
                    {
                        
                        // istrina visas sutarties uzsakytas paslaugas
                        receptoProduktaiRepository.deleteReceptoProduktus(id);

                        //per nauja prideda visas sutarties uzsakytas paslaugas
                        foreach (var item in collection.ReceptoProduktaiList)
                        {
                            item.fk_Receptasid_ = id;
                            item.id_ = receptoProduktaiRepository.getRecProdduktuCount() +100;
                            receptoProduktaiRepository.insertUzsakytaPaslauga(item);
                        }
                    }
                    else
                    {
                        receptoProduktaiRepository.deleteReceptoProduktus(id);
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

        // GET: Sutartis/Delete/5
        public ActionResult Delete(string pavadinimas)
        {
            ReceptasEditViewModel sutartis = receptaiRepository.getReceptas(pavadinimas);
            return View(sutartis);
        }

        // POST: Sutartis/Delete/5
        [HttpPost]
        public ActionResult Delete(string pavadinimas, FormCollection collection)
        {
            try
            {
                ReceptasEditViewModel receptas = receptaiRepository.getReceptas(pavadinimas);
                if (receptaiRepository.getReceptoProdCount(receptas.id_)==0)
                {
                    receptaiRepository.deleteReceptas(pavadinimas);
                }
                else
                {
                    ViewBag.naudojama = "Receptas turi produktų kuriuos naudoja, todėl pašalinti negalima";
                    return View(receptas);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(ReceptasEditViewModel receptas)
        {
            
            //surenka sarasu informacija is duomenu bazes
            var patiekalai = patiekalaiRepository.getPatiekalai();
            var produktai = produktasRepository.getProducts();
            var sudetingumas = receptoSudetingumasRepository.getReceptoSudetingumas();
            var matai = kiekioMataiRepository.getKiekioMatai();
            List<SelectListItem> selectListPatiekalai = new List<SelectListItem>();
            List<SelectListItem> selectListProduktai = new List<SelectListItem>();
            List<SelectListItem> selectListSudetingumai = new List<SelectListItem>();
            List<SelectListItem> selectListMatai = new List<SelectListItem>();

            //sukuria selectlistitem sarašus
            foreach (var item in produktai)
            {
                selectListProduktai.Add(new SelectListItem { Value = Convert.ToString(item.Pavadinimas), Text = item.Pavadinimas });
            }

            foreach (var item in sudetingumas)
            {
                selectListSudetingumai.Add(new SelectListItem { Value = Convert.ToString(item.id_), Text = item.name });
            }

            foreach (var item in patiekalai)
            {
                selectListPatiekalai.Add(new SelectListItem { Value = item.Pavadinimas, Text = item.Pavadinimas });
            }
            foreach (var item in matai)
            {
                selectListMatai.Add(new SelectListItem { Value = Convert.ToString(item.id_), Text = item.name });
            }



            //priskiria sarašus vaizdo objektui
            receptas.PatiekalaiList = selectListPatiekalai;
            receptas.Produktai = selectListProduktai;
            receptas.SudetingumaiList = selectListSudetingumai;
            receptas.KiekioMatai = selectListMatai;

            receptas.ReceptoProduktaiList = receptoProduktaiRepository.GetReceptoProduktai(receptas.id_);
        }
    }
}