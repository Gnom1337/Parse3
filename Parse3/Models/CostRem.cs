using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parse3.Models
{
    public class CostRem
    {
        [Key]
        public int id { get; set; }
        
        public string coderab { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public int nid { get; set; }
       
        
    }
}
