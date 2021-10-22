using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class MbranchView
    {
        public int SysCompanyid { get; set; }
        public string CCode { get; set; }
        public string CName { get; set; }
        public string CAddress1 { get; set; }
        public string CAddress2 { get; set; }
        public string CCity { get; set; }
        public string CArea { get; set; }
        public string CPhone { get; set; }
        public string CFax { get; set; }
        public decimal CType { get; set; }
        public string CNpwp { get; set; }
        public string CSiup { get; set; }
        public DateTime? CSiupdate { get; set; }
        public string CListing { get; set; }
        public DateTime? CListdate { get; set; }
        public DateTime? CCodate { get; set; }
        public string CSign1 { get; set; }
        public string CSign2 { get; set; }
        public string CSign3 { get; set; }
        public string CTitle1 { get; set; }
        public string CTitle2 { get; set; }
        public string CTitle3 { get; set; }
        public string Sandi { get; set; }
        public decimal? YearPeriod { get; set; }
        public decimal? MonthPeriod { get; set; }
        public string Endofmonth { get; set; }
        public string Apv1 { get; set; }
        public string Apv2 { get; set; }
        public string Apv3 { get; set; }
        public string Apv4 { get; set; }
        public string Ktp1 { get; set; }
        public string Ktp2 { get; set; }
        public string Ktp3 { get; set; }
        public string CabangOrPosko { get; set; }
        public string ParentCCode { get; set; }
        public string MedHigh { get; set; }
        public string CoaKasKecil { get; set; }
        public string CoaKasBesar { get; set; }
        public string CoaKasCollection { get; set; }
        public string CoaHubRak { get; set; }
        public string IsHo { get; set; }
        public string ShortName { get; set; }
        public string RegionCode { get; set; }
        public DateTime CreDate { get; set; }
        public string CreBy { get; set; }
        public string CreIpAddress { get; set; }
        public DateTime ModDate { get; set; }
        public string ModBy { get; set; }
        public string ModIpAddress { get; set; }
        public string IsActive { get; set; }
        public string Email { get; set; }
        public string NpwpName { get; set; }
        public string NpwpAddress { get; set; }
        public decimal? LimitPersen { get; set; }
        public string AreaCode { get; set; }
        public string CoaOperasional { get; set; }
        public string CoaCashInTransit { get; set; }
        public string CoaCashAdvance { get; set; }
    }
}
