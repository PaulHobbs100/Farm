using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Farm.Models.Entities
{
    public class BatchlistDTO 
    { 
        [Key]
    public int BatchId { get; set; }
    public string Fruit { get; set; }
    public string Variety { get; set; }
    public int Quantity { get; set; }
        //
    public int StockListId { get; set; }//

        public static explicit operator BatchlistDTO(List<BatchlistDTO> v)
        {
            throw new NotImplementedException();
        }
    }
}