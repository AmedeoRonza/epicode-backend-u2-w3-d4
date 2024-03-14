using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    public class OrdineController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Ordine
        public ActionResult Index()
        {
            var ordine = db.Ordine.Include(o => o.Bibita).Include(o => o.Pizza).Include(o => o.Users);
            return View(ordine.ToList());
        }

        // GET: Ordine/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordine ordine = db.Ordine.Find(id);
            if (ordine == null)
            {
                return HttpNotFound();
            }
            return View(ordine);
        }

        // GET: Ordine/Create
        public ActionResult Create()
        {
            ViewBag.FK_IdBibita = new SelectList(db.Bibita, "IdBibita", "Nome");
            ViewBag.FK_IdPizza = new SelectList(db.Pizza, "IdPizza", "Nome");
            ViewBag.FK_IdUtente = new SelectList(db.Users, "IdUtente", "Nome");
            return View();
        }

        // POST: Ordine/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrdine,FK_IdPizza,FK_IdBibita,FK_IdUtente,IndirizzoConsegna,Quantita,Nota,Totale")] Ordine ordine)
        {
            if (ModelState.IsValid)
            {
                db.Ordine.Add(ordine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_IdBibita = new SelectList(db.Bibita, "IdBibita", "Nome", ordine.FK_IdBibita);
            ViewBag.FK_IdPizza = new SelectList(db.Pizza, "IdPizza", "Nome", ordine.FK_IdPizza);
            ViewBag.FK_IdUtente = new SelectList(db.Users, "IdUtente", "Nome", ordine.FK_IdUtente);
            return View(ordine);
        }

        // GET: Ordine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordine ordine = db.Ordine.Find(id);
            if (ordine == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_IdBibita = new SelectList(db.Bibita, "IdBibita", "Nome", ordine.FK_IdBibita);
            ViewBag.FK_IdPizza = new SelectList(db.Pizza, "IdPizza", "Nome", ordine.FK_IdPizza);
            ViewBag.FK_IdUtente = new SelectList(db.Users, "IdUtente", "Nome", ordine.FK_IdUtente);
            return View(ordine);
        }

        // POST: Ordine/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrdine,FK_IdPizza,FK_IdBibita,FK_IdUtente,IndirizzoConsegna,Quantita,Nota,Totale")] Ordine ordine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_IdBibita = new SelectList(db.Bibita, "IdBibita", "Nome", ordine.FK_IdBibita);
            ViewBag.FK_IdPizza = new SelectList(db.Pizza, "IdPizza", "Nome", ordine.FK_IdPizza);
            ViewBag.FK_IdUtente = new SelectList(db.Users, "IdUtente", "Nome", ordine.FK_IdUtente);
            return View(ordine);
        }

        // GET: Ordine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordine ordine = db.Ordine.Find(id);
            if (ordine == null)
            {
                return HttpNotFound();
            }
            return View(ordine);
        }

        // POST: Ordine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordine ordine = db.Ordine.Find(id);
            db.Ordine.Remove(ordine);
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
