using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pizzeria.Models;

namespace Pizzeria.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PizzaController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Pizza
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Pizza.ToList());
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizza.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pizza/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPizza,Nome,Foto,Prezzo,TempoConsegna,Descrizione")] Pizza pizza, HttpPostedFileBase file)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Img"), fileName);
                    file.SaveAs(path);
                    pizza.Foto = "/Content/Img/" + fileName;
                }
                else
                {
                    pizza.Foto = "/Content/Img/Default.jpg";
                }

                if (ModelState.IsValid)
                {
                    db.Pizza.Add(pizza);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                // Gestisci l'eccezione, registrandola o visualizzando un messaggio all'utente
                Console.WriteLine("Errore durante il salvataggio del file: " + ex.Message);
            }

            // Se si arriva a questo punto, significa che c'è un errore nel modello o nel salvataggio
            return View(pizza);
        }



        // GET: Pizza/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizza.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizza/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPizza,Nome,Foto,Prezzo,TempoConsegna,Descrizione")] Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pizza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizza.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizza/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizza.Find(id);
            db.Pizza.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Carrello()
        {
            var carrello = Session["Carrello"] as List<Pizza> ?? new List<Pizza>();
            return View(carrello);
        }

        public ActionResult AddToCart(int id)
        {
            using (var dbContext = new ModelDbContext())
            {
                var pizza = dbContext.Pizza.Find(id);
                if (pizza != null)
                {
                    var carrello = Session["Carrello"] as List<Pizza> ?? new List<Pizza>();
                    carrello.Add(pizza);
                    Session["Carrello"] = carrello;

                    TempData["Message"] = "Prodotto aggiunto al carrello con successo.";
                }
                else
                {
                    TempData["Message"] = "Errore: Prodotto non trovato.";
                }

                return RedirectToAction("Index", "Pizza");
            }
        }

        public ActionResult RemoveFromCart(int id)
        {
            using (var dbContext = new ModelDbContext())
            {
                var carrello = Session["Carrello"] as List<Pizza>;
                if (carrello != null)
                {
                    // Cerca la pizza nel carrello
                    var pizzaToRemove = carrello.FirstOrDefault(p => p.IdPizza == id);
                    if (pizzaToRemove != null)
                    {
                        // Rimuovi la pizza dal carrello
                        carrello.Remove(pizzaToRemove);
                        Session["Carrello"] = carrello;

                        TempData["Message"] = "Prodotto rimosso dal carrello con successo.";
                    }
                    else
                    {
                        TempData["Message"] = "Errore: Prodotto non trovato nel carrello.";
                    }
                }
                else
                {
                    TempData["Message"] = "Errore: Carrello non trovato.";
                }

                return RedirectToAction("Index", "Pizza");
            }
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

