using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class MasterDomicile
    {
        public int? DomId { get; set; }
        public string DomType { get; set; }
        public int? DomStatus { get; set; }
    }
}
