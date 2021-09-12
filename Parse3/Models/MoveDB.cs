using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Parse3.Models
{
    public class MoveDB
    {
        
        public int id { get; set; }
        public string coderem { get; set; }
        public string coderab { get; set; }
        public int name { get; set; }

    }
}
