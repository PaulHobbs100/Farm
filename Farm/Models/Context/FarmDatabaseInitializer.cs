using System.Data.Entity;
using Farm.Models.Entities;
using System.Data.Entity.Migrations;

namespace Farm.Models.Context
{
    public class FarmDatabaseInitializer :DropCreateDatabaseIfModelChanges<FarmContext>
    {
        protected override void Seed(FarmContext context)
        {
            // Seed tables to match test data example

            context.StockItems.AddOrUpdate(x=>x.StockItemId,
                new StockItem() { StockItemId = 1, Fruit = "Raspberry", Variety = "Amira" },
                new StockItem() { StockItemId = 2, Fruit = "Raspberry", Variety = "Erika" },
                new StockItem() { StockItemId = 3, Fruit = "Blueberry", Variety = "Alba" }
                );

            context.Batches.AddOrUpdate(x => x.BatchId,
                new Batch() { BatchId = 1, StockItemId = 1, Quantity = 12},
                new Batch() { BatchId = 2, StockItemId = 2, Quantity = 10 },
                new Batch() { BatchId = 3, StockItemId = 1, Quantity = 10 },
                new Batch() { BatchId = 4, StockItemId = 3, Quantity = 15 }
                );
         }
    }
}