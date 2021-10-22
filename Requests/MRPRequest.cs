using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Requests
{
    public class MRPRequest
    {
        public string merkId { get; set; }
        public string modelId { get; set; }
        public string typeId { get; set; }
        public string year { get; set; }
        public string branchId { get; set; }
    }
}
