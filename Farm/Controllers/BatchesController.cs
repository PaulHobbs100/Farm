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
using log4net;

namespace Farm.Controllers
{
    public class BatchesController : ApiController
    {
        private FarmContext db = new FarmContext();
        public ILog logger = log4net.LogManager.GetLogger("ErrorLog");

        // GET: api/Batches
        /// <summary>
        /// Get All Batches
        /// </summary>
        /// <returns></returns>
        public IQueryable<BatchlistDTO> GetBatches()
        {
            try
            {
                logger.Info("Get batches requested");
             
                var batchlist = from Batch in db.Batches
                                join StockItem in db.StockItems
                                on Batch.StockItemId equals StockItem.StockItemId
                                orderby Batch.BatchId
                                select new BatchlistDTO
                                {
                                    BatchId=    Batch.BatchId,
                                    Fruit=   StockItem.Fruit,
                                    Variety= StockItem.Variety,
                                    Quantity=  Batch.Quantity

                                };

                return  batchlist; ;


            }
            catch (Exception ex)
            {
                logger.Error("Get batches failed:" + ex.Message);
                return null;
            }
            finally
            {
                logger.Info("Get batches complete");
            }
           
            
           
        }
        // GET: api/Batches/5
        /// <summary>
        /// Get Individualt Batch details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All Batches</returns>
     
        [ResponseType(typeof(Batch))]
        public IHttpActionResult GetBatch(int id)
        {
            var batchitem = from Batch in db.Batches
                            join StockItem in db.StockItems
                            on Batch.StockItemId equals StockItem.StockItemId
                            where Batch.BatchId == id
                            orderby Batch.BatchId
                            select new BatchlistDTO
                            {
                                BatchId = Batch.BatchId,
                                Fruit = StockItem.Fruit,
                                Variety = StockItem.Variety,
                                Quantity = Batch.Quantity

                            };

            //Batch batch = db.Batches.Find(id);
            logger.Info("Getting batch item id=" + id);
            if (batchitem == null)
            {
                logger.Warn("Batch item id=" + id + " not found");
                return NotFound();
            }
            logger.Info("Got batch item id=" + id);
            //return Ok(batch);
            return Ok(batchitem);
        }

        // PUT: api/Batches/5
        /// <summary>
        /// Update Batch details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="batch"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBatch(int id, Batch batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != batch.BatchId)
            {
                return BadRequest();
            }

            db.Entry(batch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(id))
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

        // POST: api/Batches
        /// <summary>
        /// Create new Batch
        /// </summary>
        /// <param name="batch"></param>
        /// <returns></returns>
        [ResponseType(typeof(Batch))]
        public IHttpActionResult PostBatch(Batch batch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Batches.Add(batch);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = batch.BatchId }, batch);
        }

        // DELETE: api/Batches/5
        /// <summary>
        /// DElete Batch item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Batch))]
        public IHttpActionResult DeleteBatch(int id)
        {
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                logger.Warn("Delete batch item failed id=" + id);
                return NotFound();
            }

            db.Batches.Remove(batch);
            db.SaveChanges();
            logger.Info("Deleted batch item id=" + id);
            return Ok(batch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BatchExists(int id)
        {
            return db.Batches.Count(e => e.BatchId == id) > 0;
        }
    }
}