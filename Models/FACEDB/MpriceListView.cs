using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class MpriceListView
    {
        public string CCode { get; set; }
        public string Branch { get; set; }
        public string PackageCode { get; set; }
        public string ProductCode { get; set; }
        public string CategoryCode { get; set; }
        public string SubCategoryCode { get; set; }
        public string ClassCode { get; set; }
        public string MerkCode { get; set; }
        public string ModelCode { get; set; }
        public string TypeCode { get; set; }
        public string AssYear { get; set; }
        public string Condition { get; set; }
        public string Grade { get; set; }
        public decimal Price { get; set; }
        public decimal MaxDp { get; set; }
        public decimal MaxPurchase { get; set; }
        public string Status { get; set; }
        public decimal ChangePrice { get; set; }
        public decimal ChangeMaxDp { get; set; }
        public decimal ChangeMaxPurchase { get; set; }
        public decimal Rate { get; set; }
        public decimal ChangeRate { get; set; }
        public int Tenor { get; set; }
        public decimal Rentalpay { get; set; }
        public decimal ExtRate { get; set; }
        public decimal ChangeExtRate { get; set; }
        public DateTime? EffDate { get; set; }
        public DateTime CreDate { get; set; }
        public string CreBy { get; set; }
        public string CreIpAddress { get; set; }
        public DateTime ModDate { get; set; }
        public string ModBy { get; set; }
        public string ModIpAddress { get; set; }
        public decimal? BatasToleransiPct { get; set; }
        public string MarketPrice { get; set; }
        public string CollateralAtasNama { get; set; }
        public decimal? BatasToleransiAmt { get; set; }
        public decimal? MaxPengajuanPhAmt { get; set; }
        public decimal? ChangeBatasToleransiPct { get; set; }
    }
}
