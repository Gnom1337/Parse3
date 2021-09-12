using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parse3.Models
{
    public class ViewModel
    {
        public IEnumerable<CostRem> CostRems { get; set; }
       
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Price> Prices { get; set; }
        public IEnumerable<MoveDB> MoveDBs { get; set; }
    }
}
