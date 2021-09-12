using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parse3.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        public int num { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string nameCustomers { get; set; }
    }
}
