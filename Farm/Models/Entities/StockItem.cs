using System.ComponentModel.DataAnnotations;

namespace Farm.Models.Entities
{
    public class StockItem
    {
        [Key]
        public int StockItemId { get; set; }
        public string Fruit { get; set; }
        public string Variety { get; set; }
    }
}