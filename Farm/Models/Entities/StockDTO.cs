using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farm.Models.Entities
{
    public class StockDTO
    {   [Key]
        public int StockItemId { get; set; }
        public string Fruit { get; set; }
        public string Variety { get; set; }
        public int Quantity { get; set; }
    }
}