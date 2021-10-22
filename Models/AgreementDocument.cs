using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Models
{
    public class AgreementDocument
    {
        public string filename { get; set; }
        public int flag { get; set; }
        public string Message { get; set; }
        public byte[] data { get; set; }
    }
}
