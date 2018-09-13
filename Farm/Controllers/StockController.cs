using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Farm.Models.Context;
using Farm.Models.Entities;

namespace Farm.Controllers
{
    public class StockController : Controller
    {
        private FarmContext db = new FarmContext();

        // GET: Stock
        public ActionResult Index()
        {


            var batchlist = from Batch in db.Batches
                            join StockItem in db.StockItems
                            on Batch.StockItemId equals StockItem.StockItemId
                            group  Batch by new { Batch.StockItemId, StockItem.Fruit, StockItem.Variety }
                            into G
                            let tots =G.Sum(m=>m.Quantity)
                                                  
                            select new StockDTO
                            {
                                StockItemId = G.Key.StockItemId,
                                Fruit = G.Key.Fruit,
                                Variety = G.Key.Variety,
                                Quantity = tots

                            };

            //return View(db.StockItems.ToList());
            return View(batchlist.ToList());


            //var stocklist = from StockItem in db.StockItems
            //                join Batch in db.Batches
            //                on StockItem.StockItemId equals Batch.StockItemId
            //                group StockItem by new { StockItem.StockItemId }
            //                into grouped
            //                select new StockDTO
            //                {
            //                    StockItemId = 3,
            //                    Fruit = StockItem
            //                };
        }

        // GET: Stock/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // GET: Stock/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Stock/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StockItemId,Fruit,Variety")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.StockItems.Add(stockItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stockItem);
        }

        // GET: Stock/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: Stock/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StockItemId,Fruit,Variety")] StockItem stockItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stockItem);
        }

        // GET: Stock/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return HttpNotFound();
            }
            return View(stockItem);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockItem stockItem = db.StockItems.Find(id);
            db.StockItems.Remove(stockItem);
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
