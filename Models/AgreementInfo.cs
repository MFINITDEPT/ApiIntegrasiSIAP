using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Models
{
    public class AgreementInfo
    {
        public DataTable data { get; set; }
        public int flag { get; set; }
        public string message { get; set; }
    }
}
