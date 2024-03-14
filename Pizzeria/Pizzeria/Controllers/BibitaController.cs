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
    public class BibitaController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Bibita
        public ActionResult Index()
        {
            return View(db.Bibita.ToList());
        }

        // GET: Bibita/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibita bibita = db.Bibita.Find(id);
            if (bibita == null)
            {
                return HttpNotFound();
            }
            return View(bibita);
        }

        // GET: Bibita/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bibita/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBibita,Nome,Foto,Prezzo")] Bibita bibita)
        {
            if (ModelState.IsValid)
            {
                db.Bibita.Add(bibita);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bibita);
        }

        // GET: Bibita/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibita bibita = db.Bibita.Find(id);
            if (bibita == null)
            {
                return HttpNotFound();
            }
            return View(bibita);
        }

        // POST: Bibita/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBibita,Nome,Foto,Prezzo")] Bibita bibita)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bibita).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bibita);
        }

        // GET: Bibita/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibita bibita = db.Bibita.Find(id);
            if (bibita == null)
            {
                return HttpNotFound();
            }
            return View(bibita);
        }

        // POST: Bibita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bibita bibita = db.Bibita.Find(id);
            db.Bibita.Remove(bibita);
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
