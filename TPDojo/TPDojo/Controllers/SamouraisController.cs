using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;//chargement en mode eager-include(x=>x.Id)...
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TPDojo.Data;
using TPDojo.Models;
using TPDojoBO;

namespace TPDojo.Controllers
{
    public class SamouraisController : Controller
    {
        //si static un db pour tous les clients => contexte jamais fermé =>sans savechange() mais utilise db en memoire
        //donc fluid de donnée d'un client à l'autre
        private Context db = new Context();

        // GET: Samourais
        public ActionResult Index()
        {
            return View(db.Samourais.ToList());
        }

        // GET: Samourais/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // GET: Samourais/Create
        public ActionResult Create()
        {
            SamouraiViewModel vm = new SamouraiViewModel();
            List<long> idsArmes = db.Samourais.Where(s => s.Arme != null).Select(s => s.Arme.Id).ToList();
            vm.Armes = db.Armes.Where(a => !idsArmes.Contains(a.Id)).
                Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            vm.ArtMartials.Add(new ArtMartial() { Nom = "Aucun" });
            vm.ArtMartials.AddRange(db.ArtMartials.ToList());
            
            return View(vm);
        }

        // POST: Samourais/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SamouraiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Samourai samourai = vm.Samourai;
                if (vm.IdArme!=null)
                {
                    //samourai.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdArme);
                    var samourais = db.Samourais.Where(s => s.Arme.Id == vm.IdArme).ToList();
                    foreach (var item in samourais)
                    {
                        item.Arme = null;
                        db.Entry(item).State = EntityState.Modified;
                    }
                    samourai.Arme = db.Armes.Find(vm.IdArme);
                }
                samourai.ArtMartials = db.ArtMartials.Where(a => vm.IdsArtMartials.Contains(a.Id)).ToList();
                db.Samourais.Add(samourai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<long> IdsArmes = db.Samourais.Where(s => s.Arme != null).Select(s => s.Arme.Id).ToList();
            //vm.Armes = db.Armes.Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            vm.Armes = db.Armes.Where(a => !IdsArmes.Contains(a.Id)).
                Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            vm.ArtMartials.Add(new ArtMartial() { Nom = "Aucun" });
            vm.ArtMartials.AddRange(db.ArtMartials.ToList());
            return View(vm);
        }

        // GET: Samourais/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SamouraiViewModel vm = new SamouraiViewModel();
            vm.Armes = db.Armes.Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();
            
            vm.Samourai = db.Samourais.Find(id);
            if (vm.Samourai.Arme != null)
            {
                vm.IdArme = vm.Samourai.Arme.Id;
            }
            if (vm.Samourai == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Samourais/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SamouraiViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //Samourai samourai = db.Samourais.Find(vm.Samourai.Id);
                //chargement des armes en mode eager
                Samourai samourai = db.Samourais.Include(x=>x.Arme).FirstOrDefault(x=>x.Id==vm.Samourai.Id); 
                samourai.Force = vm.Samourai.Force;
                samourai.Nom = vm.Samourai.Nom;
                if (vm.IdArme!=null)
                {
                    Arme arme = null;
                    if (arme == null)
                    {
                        samourai.Arme = db.Armes.FirstOrDefault(x => x.Id == vm.IdArme);
                    }
                    else
                    {
                        samourai.Arme = arme;
                    }
                }
                else
                {
                    samourai.Arme = null;
                }
                db.Entry(samourai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Samourais/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Samourai samourai = db.Samourais.Find(id);
            if (samourai == null)
            {
                return HttpNotFound();
            }
            return View(samourai);
        }

        // POST: Samourais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Samourai samourai = db.Samourais.Find(id);
            db.Samourais.Remove(samourai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
