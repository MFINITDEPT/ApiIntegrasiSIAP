using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class SiapDm
    {
        public long EntryId { get; set; }
        public long? SiapId { get; set; }
        public string ProductName { get; set; }
        public string DomCode { get; set; }
        public decimal? SubmittedAmount { get; set; }
        public decimal? Fee { get; set; }
        public int? Tenor { get; set; }
        public decimal? EstimatedInstallment { get; set; }
        public decimal? EstimatedDisbursement { get; set; }
        public string CarRegistrationNumber { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string CarYear { get; set; }
        public string CarType { get; set; }
        public string CarInsurance { get; set; }
        public string StnkPhoto { get; set; }
        public string CarPhoto { get; set; }
        public string FullName { get; set; }
        public string NikNumber { get; set; }
        public string NpwpNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string KtpPhoto { get; set; }
        public string SelfieWithKtpPhoto { get; set; }
        public string NpwpPhoto { get; set; }
        public string PlaceOfBirth { get; set; }
        public string MothersName { get; set; }
        public string SpouseName { get; set; }
        public string SpouseNikNumber { get; set; }
        public string JobType { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public decimal? MonthlyIncome { get; set; }
        public string CurrentFormStep { get; set; }
        public string ApplicationStatus { get; set; }
        public string EkycEmail { get; set; }
        public string EkycStatus { get; set; }
        public string EkycRejectReason { get; set; }
        public string IdProvince { get; set; }
        public string IdProvinceCode { get; set; }
        public string IdDistrict { get; set; }
        public string IdDistrictCode { get; set; }
        public string IdSubdistrict1 { get; set; }
        public string IdSubdistrict1Code { get; set; }
        public string IdSubdistrict2 { get; set; }
        public string IdSubdistrict2Code { get; set; }
        public string IdRt { get; set; }
        public string IdRw { get; set; }
        public string IdLat { get; set; }
        public string IdLng { get; set; }
        public string IdAddress { get; set; }
        public string IdPostalCode { get; set; }
        public string DomOwnership { get; set; }
        public string DomProvince { get; set; }
        public string DomProvinceCode { get; set; }
        public string DomDistrict { get; set; }
        public string DomDistrictCode { get; set; }
        public string DomSubdistrict1 { get; set; }
        public string DomSubdistrict1Code { get; set; }
        public string DomSubdistrict2 { get; set; }
        public string DomSubdistrict2Code { get; set; }
        public string DomRt { get; set; }
        public string DomRw { get; set; }
        public string DomLat { get; set; }
        public string DomLng { get; set; }
        public string DomAddress { get; set; }
        public string DomPostalCode { get; set; }
        public string IsExpressSurvey { get; set; }
        public DateTime? SurveyDate { get; set; }
        public string PlnNumber { get; set; }
        public string PamNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string Source { get; set; }
        public string MfinState { get; set; }
        public string DigisignFile { get; set; }
        public string CreBy { get; set; }
        public DateTime? CreDate { get; set; }
        public string CreIp { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModDate { get; set; }
        public string ModIp { get; set; }
    }
}
