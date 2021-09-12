using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parse3.Models
{
    public class Customer
    {
        
        public int id { get; set; }
        public int nid1 { get; set; }
        public string name { get; set; }
        public string Date { get; set; }
        public int EndCost { get; set; }
        
    }
}
