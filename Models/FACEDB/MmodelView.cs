using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class MmodelView
    {
        public string ModelCode { get; set; }
        public string MerkCode { get; set; }
        public string CategoryCode { get; set; }
        public string Description { get; set; }
        public DateTime? CreDate { get; set; }
        public string CreBy { get; set; }
        public string CreIpAddress { get; set; }
        public DateTime? ModDate { get; set; }
        public string ModBy { get; set; }
        public string ModIpAddress { get; set; }
        public string Grade { get; set; }
    }
}
