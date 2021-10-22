using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class MmerkView
    {
        public string MerkCode { get; set; }
        public string Description { get; set; }
        public string CountryCode { get; set; }
        public DateTime? CreDate { get; set; }
        public string CreBy { get; set; }
        public string CreIpAddress { get; set; }
        public DateTime? ModDate { get; set; }
        public string ModBy { get; set; }
        public string ModIpAddress { get; set; }
    }
}
