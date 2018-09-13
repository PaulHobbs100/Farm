using Farm.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Farm.Models.Context
{
    public class FarmContext :DbContext
    {
        public FarmContext() : base("Farm") { }

        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Batch> Batches { get; set; }
    }
}