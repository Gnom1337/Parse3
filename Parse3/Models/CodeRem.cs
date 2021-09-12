using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parse3.Models
{
    public class CodeRem
    {
        
        
        [Key]
        public int id { get;set; }
        public string coderem { get; set; }
        public int name { get; set; }
     
        
    }
}
