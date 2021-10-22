using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class TapplCStatus
    {
        public string ApplId { get; set; }
        public string PoliceNo { get; set; }
        public string MsixApplicno { get; set; }
        public string MsixLsagree { get; set; }
        public string ApplStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
