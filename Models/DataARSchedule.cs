using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Models
{
    public class DataARSchedule
    {
        public string TanggalMulai { get; set; }
        public string TotalHutang { get; set; }
        public string TipeUnit { get; set; }
        public string Rental { get; set; }
        public DataTable data { get; set; }
        public int flag { get; set; }
        public string message { get; set; }
    }
}
