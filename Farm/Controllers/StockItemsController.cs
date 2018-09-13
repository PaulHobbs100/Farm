using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Farm.Models.Context;
using Farm.Models.Entities;

namespace Farm.Controllers
{
    public class StockItemsController : ApiController
    {
        private FarmContext db = new FarmContext();

        // GET: api/StockItems
        /// <summary>
        /// Get all stock items
        /// </summary>
        /// <returns></returns>
        public IQueryable<StockItem> GetStockItems()
        {
            return db.StockItems;
        }

        // GET: api/StockItems/5
        /// <summary>
        /// Get specific Stock Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(StockItem))]
        public IHttpActionResult GetStockItem(int id)
        {
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            return Ok(stockItem);
        }

        // PUT: api/StockItems/5
            /// <summary>
            /// Update specific stock item
            /// </summary>
            /// <param name="id"></param>
            /// <param name="stockItem"></param>
            /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStockItem(int id, StockItem stockItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stockItem.StockItemId)
            {
                return BadRequest();
            }

            db.Entry(stockItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/StockItems
        /// <summary>
        /// Create new stock item
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        [ResponseType(typeof(StockItem))]
        public IHttpActionResult PostStockItem(StockItem stockItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StockItems.Add(stockItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stockItem.StockItemId }, stockItem);
        }

        // DELETE: api/StockItems/5
        /// <summary>
        /// Delete specific stock item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(StockItem))]
        public IHttpActionResult DeleteStockItem(int id)
        {
            StockItem stockItem = db.StockItems.Find(id);
            if (stockItem == null)
            {
                return NotFound();
            }

            db.StockItems.Remove(stockItem);
            db.SaveChanges();

            return Ok(stockItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StockItemExists(int id)
        {
            return db.StockItems.Count(e => e.StockItemId == id) > 0;
        }
    }
}