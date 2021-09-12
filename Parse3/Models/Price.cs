using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parse3.Models
{
    public class Price
    {
        [Key]
        public int id { get; set; }
        
        public string coderab { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        
        
    }
}
