using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Farm.Models.Context;
using Farm.Models.Entities;

namespace Farm.Controllers
{
    public class DashboardController : Controller
    {
        private FarmContext db = new FarmContext();

        // GET: Dashboard
        public ActionResult Index()
        {
            //var batches = db.Batches.Include(b => b.StockItem);
            //return View(batches.ToList());

            IEnumerable<BatchlistDTO> batchlist = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52789/api/");
                var responseTask = client.GetAsync("batches");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<BatchlistDTO>>();
                    readTask.Wait();
                    batchlist = readTask.Result;

                }else
                {

                }
                return View(batchlist);
            }




        }





        // GET: Dashboard/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);


        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            ViewBag.StockItemId = new SelectList(db.StockItems, "StockItemId", "Fruit", "variety, batch.StockItemId");
            ViewBag.Plants = db.StockItems.ToList();
            return View();
        }

        // POST: Dashboard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchId,StockItemId,Quantity")] Batch batch)
        {
            if (ModelState.IsValid)
            {

               
                db.Batches.Add(batch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StockItemId = new SelectList(db.StockItems, "StockItemId", "Fruit");
            return View(batch);
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockItemId = new SelectList(db.StockItems, "StockItemId", "Fruit", batch.StockItemId);
            return View(batch);
        }

        // POST: Dashboard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchId,StockItemId,Quantity")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockItemId = new SelectList(db.StockItems, "StockItemId", "Fruit", batch.StockItemId);
            return View(batch);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Dashboard/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            return View(batch);
        }

        // POST: Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Batch batch = db.Batches.Find(id);
            db.Batches.Remove(batch);
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
