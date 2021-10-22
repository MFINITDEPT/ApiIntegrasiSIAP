using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class MasterInsurance
    {
        public Guid InsuranceId { get; set; }
        public string Desc { get; set; }
        public string InsuranceType { get; set; }
    }
}
