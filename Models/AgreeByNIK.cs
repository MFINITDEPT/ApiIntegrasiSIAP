using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Models
{
    public class AgreeByNIK
    {
        public string nik { get; set; }
        public string lessee { get; set; }
        public DataNIK[] datanik { get; set; }
        public int flag { get; set; }
        public string message { get; set; }
    }
}
