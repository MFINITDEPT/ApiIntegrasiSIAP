using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Models
{
    public class UploadDanaRumah
    {
        public long entry_id { get; set; }
        public long siap_id { get; set; }
        public string product_name { get; set; }
        public double? submitted_amount { get; set; }
        public double? fee { get; set; }
        public int? tenor { get; set; }
        public double? estimated_installment { get; set; }
        public double? estimated_disbursement { get; set; }
        public string ast_province { get; set; }
        public string ast_province_code { get; set; }
        public string ast_district { get; set; }
        public string ast_district_code { get; set; }
        public string ast_subdistrict1 { get; set; }
        public string ast_subdistrict1_code { get; set; }
        public string ast_subdistrict2 { get; set; }
        public string ast_subdistrict2_code { get; set; }
        public string ast_rt { get; set; }
        public string ast_rw { get; set; }
        public string ast_lat { get; set; }
        public string ast_lng { get; set; }
        public string ast_address { get; set; }
        public string ast_postal_code { get; set; }
        public string full_name { get; set; }
        public string nik_number { get; set; }
        public string npwp_number { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string mobile_number { get; set; }
        public DateTime? birth_date { get; set; }
        public string place_of_birth { get; set; }
        public string mothers_name { get; set; }
        public string SpouseName { get; set; }
        public string spouseNik_number { get; set; }
        public string JobType { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public double monthly_income { get; set; }
        public string current_form_step { get; set; }
        public string application_status { get; set; }
        public string ekyc_email { get; set; }
        public string ekyc_status { get; set; }
        public string ekyc_reject_reason { get; set; }
        public string id_province { get; set; }
        public string id_province_code { get; set; }
        public string id_district { get; set; }
        public string id_district_code { get; set; }
        public string id_subdistrict1 { get; set; }
        public string id_subdistrict1_code { get; set; }
        public string id_subdistrict2 { get; set; }
        public string id_subdistrict2_code { get; set; }
        public string id_rt { get; set; }
        public string id_rw { get; set; }
        public string id_lat { get; set; }
        public string id_lng { get; set; }
        public string id_address { get; set; }
        public string id_postal_code { get; set; }
        public string dom_ownership { get; set; }
        public string dom_province { get; set; }
        public string dom_province_code { get; set; }
        public string dom_district { get; set; }
        public string dom_district_code { get; set; }
        public string dom_subdistrict1 { get; set; }
        public string dom_subdistrict1_code { get; set; }
        public string dom_subdistrict2 { get; set; }
        public string dom_subdistrict2_code { get; set; }
        public string dom_rt { get; set; }
        public string dom_rw { get; set; }
        public string dom_lat { get; set; }
        public string dom_lng { get; set; }
        public string dom_address { get; set; }
        public string dom_postal_code { get; set; }
        public string PLN_Number { get; set; }
        public string PAM_Number { get; set; }
        public string bank_name { get; set; }
        public string bank_account_number { get; set; }
        public string bank_account_name { get; set; }
        public string source { get; set; }
        public string mfin_state { get; set; }
        public string cre_by { get; set; }
        public DateTime? cre_date { get; set; }
        public string cre_ip { get; set; }
        public string mod_by { get; set; }
        public DateTime? mod_date { get; set; }
        public string mod_ip { get; set; }

        public Dictionary<String, String> Validate(HttpContext http)
        {
            var files = http.Request.Form.Files;
            Dictionary<String, String> validationError = new Dictionary<string, string>() { };
            List<string> mandatoryPhoto = new List<string>() { "pbb_photo", "house_photo", "ktp_photo", "selfie_with_ktp_photo" };

            if (mandatoryPhoto != null && mandatoryPhoto.Count() > 0)
            {
                foreach (var item in mandatoryPhoto)
                {
                    if (files.Where(a => a.Name == item).FirstOrDefault() == null)
                    {
                        validationError.Add(item, item + " harus diunggah!");
                    }
                }
            }

            return validationError;
        }
    }
}
