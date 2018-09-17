using System.ComponentModel.DataAnnotations;

namespace Farm.Models.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Batch
    {
        [Key]
        public int BatchId { get; set; }
        //Foreign key
        public int StockItemId { get; set; }
        //Navigation property
        public StockItem StockItem { get; set; }
        //public string Fruit { get; set; }   //
        //public string Variety { get; set; } //
    
        public int Quantity { get; set; }
    }
}