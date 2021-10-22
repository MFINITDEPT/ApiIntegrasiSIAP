using ApiIntegrasiSIAP.Models;
using ApiIntegrasiSIAP.Models.FACEDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Repositories
{
    public class SiapRepository
    {
        public static FACEDBContext db;
        public static IWebHostEnvironment hostingEnv { get; set; }

        public static Int64 CreateDanaRumah(UploadDanaRumah request, HttpContext http)
        {
            var item = new SiapDr();
            IFormFileCollection files = http.Request.Form.Files;

            String separator = "\\";

            String uploadPath = hostingEnv.ContentRootPath + separator + "Upload";

            #region Create Object
            item.SiapId = request.siap_id;
            item.ProductName = request.product_name;
            item.SubmittedAmount = Convert.ToDecimal(request.submitted_amount);
            item.Fee = Convert.ToDecimal(request.fee);
            item.Tenor = request.tenor;
            item.EstimatedInstallment = Convert.ToDecimal(request.estimated_installment);
            item.EstimatedDisbursement = Convert.ToDecimal(request.estimated_disbursement);
            item.AstProvince = request.ast_province;
            item.AstProvinceCode = request.ast_province_code;
            item.AstDistrict = request.ast_district;
            item.AstDistrictCode = request.ast_district_code;
            item.AstSubdistrict1 = request.ast_subdistrict1;
            item.AstSubdistrict1Code = request.ast_subdistrict1_code;
            item.AstSubdistrict2 = request.ast_subdistrict2;
            item.AstSubdistrict2Code = request.ast_subdistrict2_code;
            item.AstRt = request.ast_rt;
            item.AstRw = request.ast_rw;
            item.AstLat = request.ast_lat;
            item.AstLng = request.ast_lng;
            item.AstAddress = request.ast_address;
            item.AstPostalCode = request.ast_postal_code;
            item.FullName = request.full_name;
            item.NikNumber = request.nik_number;
            item.NpwpNumber = request.npwp_number;
            item.Email = request.email;
            item.PhoneNumber = request.phone_number;
            item.MobileNumber = request.mobile_number;
            item.BirthDate = request.birth_date;
            item.PlaceOfBirth = request.place_of_birth;
            item.MothersName = request.mothers_name;
            item.SpouseName = request.SpouseName;
            item.SpouseNikNumber = request.spouseNik_number;
            item.JobType = request.JobType;
            item.CompanyName = request.CompanyName;
            item.CompanyAddress = request.CompanyAddress;
            item.CompanyPhone = request.CompanyPhone;
            item.MonthlyIncome = Convert.ToDecimal(request.monthly_income);
            item.CurrentFormStep = request.current_form_step;
            item.ApplicationStatus = request.application_status;
            item.EkycEmail = request.ekyc_email;
            item.EkycStatus = request.ekyc_status;
            item.EkycRejectReason = request.ekyc_reject_reason;
            item.IdProvince = request.id_province;
            item.IdProvinceCode = request.id_province_code;
            item.IdDistrict = request.id_district;
            item.IdDistrictCode = request.id_district_code;
            item.IdSubdistrict1 = request.id_subdistrict1;
            item.IdSubdistrict1Code = request.id_subdistrict1_code;
            item.IdSubdistrict2 = request.id_subdistrict2;
            item.IdSubdistrict2Code = request.id_subdistrict2_code;
            item.IdRt = request.id_rt;
            item.IdRw = request.id_rw;
            item.IdLat = request.id_lat;
            item.IdLng = request.id_lng;
            item.IdAddress = request.id_address;
            item.IdPostalCode = request.id_postal_code;
            item.DomOwnership = request.dom_ownership;
            item.DomProvince = request.dom_province;
            item.DomProvinceCode = request.dom_province_code;
            item.DomDistrict = request.dom_district;
            item.DomDistrictCode = request.dom_district_code;
            item.DomSubdistrict1 = request.dom_subdistrict1;
            item.DomSubdistrict1Code = request.dom_subdistrict1_code;
            item.DomSubdistrict2 = request.dom_subdistrict2;
            item.DomSubdistrict2Code = request.dom_subdistrict2_code;
            item.DomRt = request.dom_rt;
            item.DomRw = request.dom_rw;
            item.DomLat = request.dom_lat;
            item.DomLng = request.dom_lng;
            item.DomAddress = request.dom_address;
            item.DomPostalCode = request.dom_postal_code;
            item.PlnNumber = request.PLN_Number;
            item.PamNumber = request.PAM_Number;
            item.BankName = request.bank_name;
            item.BankAccountNumber = request.bank_account_number;
            item.BankAccountName = request.bank_account_name;
            item.Source = request.source;
            item.MfinState = request.mfin_state;
            item.CreBy = request.cre_by;
            item.CreDate = DateTime.Now;
            item.CreIp = http.Request.Headers["REMOTE_ADDR"];
            item.ModBy = request.mod_by;
            item.ModDate = DateTime.Now;
            item.ModIp = http.Request.Headers["REMOTE_ADDR"];
            #endregion

            //List<string> listKey = new List<string>() { "PbbPhoto", "HousePhoto", "KtpPhoto", "SelfieWithKtpPhoto" };

            // Folder Upload
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            // Folder Dana Rumah
            uploadPath += separator + "DanaRumah";
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            // Folder entry
            uploadPath += separator + item.SiapId;
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            #region FileUpload
            var PbbPhoto = files.Where(a => a.Name == "pbb_photo").FirstOrDefault();
            if (PbbPhoto != null)
            {
                String[] arrSplit = PbbPhoto.FileName.Split(".");
                String type = "_PbbPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.PbbPhoto = filepath;

                //System.IO.File.Create(item.PbbPhoto);
                using (Stream filestream = new FileStream(filepath, FileMode.Create))
                {
                    PbbPhoto.CopyTo(filestream);
                }
            }
            else
            {
                throw new Exception("PbbPhoto harus diuanggah!");
            }

            var HousePhoto = files.Where(a => a.Name == "house_photo").FirstOrDefault();
            if (HousePhoto != null)
            {
                String[] arrSplit = HousePhoto.FileName.Split(".");
                String type = "_HousePhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.HousePhoto = filepath;

                //System.IO.File.Create(item.HousePhoto);
                using (Stream filestream = new FileStream(filepath, FileMode.Create))
                {
                    HousePhoto.CopyTo(filestream);
                }
            }
            else
            {
                throw new Exception("HousePhoto harus diuanggah!");
            }

            var KtpPhoto = files.Where(a => a.Name == "ktp_photo").FirstOrDefault();
            if (KtpPhoto != null)
            {
                String[] arrSplit = KtpPhoto.FileName.Split(".");
                String type = "_KtpPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.KtpPhoto = filepath;

                //System.IO.File.Create(item.KtpPhoto);
                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    KtpPhoto.CopyTo(fileStream);
                }
            }
            else
            {
                throw new Exception("KtpPhoto harus diuanggah!");
            }

            var SelfieWithKtpPhoto = files.Where(a => a.Name == "selfie_with_ktp_photo").FirstOrDefault();
            if (SelfieWithKtpPhoto != null)
            {
                String[] arrSplit = SelfieWithKtpPhoto.FileName.Split(".");
                String type = "_SelfieWithKtpPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.SelfieWithKtpPhoto = filepath;

                //System.IO.File.Create(item.SelfieWithKtpPhoto);
                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    SelfieWithKtpPhoto.CopyTo(fileStream);
                }
            }
            else
            {
                throw new Exception("SelfieWithKtpPhoto harus diuanggah!");
            }

            var npwpFile = files.Where(a => a.Name == "npwp_photo").FirstOrDefault();
            if (npwpFile != null)
            {
                String[] arrSplit = npwpFile.FileName.Split(".");
                String type = "_NpwpPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" +DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                string filepath = (uploadPath + separator + filename + ext);
                item.NpwpPhoto = filepath;

                //System.IO.File.Create(item.NpwpPhoto);
                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    npwpFile.CopyTo(fileStream);
                }
            }
            #endregion

            db.SiapDrs.Add(item);

            db.SaveChanges();

            return item.EntryId;
        }

        public static Int64 CreateDanaMobil(UploadDanaMobil request, HttpContext http)
        {
            var item = new SiapDm();
            IFormFileCollection files = http.Request.Form.Files;

            String separator = "\\";

            String uploadPath = hostingEnv.WebRootPath + separator + "Upload";

            #region Create Object
            item.SiapId = request.siap_id;
            item.ProductName = request.product_name;
            item.DomCode = request.dom_code;
            item.SubmittedAmount = request.submitted_amount;
            item.Fee = request.fee;
            item.Tenor = request.tenor;
            item.EstimatedInstallment = request.estimated_installment;
            item.EstimatedDisbursement = request.estimated_disbursement;
            item.CarRegistrationNumber = request.car_registration_number;
            item.CarBrand = request.car_brand;
            item.CarModel = request.car_model;
            item.CarYear = request.car_year;
            item.CarType = request.car_type;
            item.CarInsurance = request.car_insurance;
            item.StnkPhoto = request.stnk_photo;
            item.CarPhoto = request.car_photo;
            item.FullName = request.full_name;
            item.NikNumber = request.nik_number;
            item.NpwpNumber = request.npwp_number;
            item.Email = request.email;
            item.PhoneNumber = request.phone_number;
            item.MobileNumber = request.mobile_number;
            item.BirthDate = request.birth_date;
            item.KtpPhoto = request.ktp_photo;
            item.SelfieWithKtpPhoto = request.selfie_with_ktp_photo;
            item.NpwpPhoto = request.npwp_photo;
            item.PlaceOfBirth = request.place_of_birth;
            item.MothersName = request.mothers_name;
            item.SpouseName = request.SpouseName;
            item.SpouseNikNumber = request.spouseNik_number;
            item.JobType = request.JobType;
            item.CompanyName = request.CompanyName;
            item.CompanyAddress = request.CompanyAddress;
            item.CompanyPhone = request.CompanyPhone;
            item.MonthlyIncome = request.monthly_income;
            item.CurrentFormStep = request.current_form_step;
            item.ApplicationStatus = request.application_status;
            item.EkycEmail = request.ekyc_email;
            item.EkycStatus = request.ekyc_status;
            item.EkycRejectReason = request.ekyc_reject_reason;
            item.IdProvince = request.id_province;
            item.IdProvinceCode = request.id_province_code;
            item.IdDistrict = request.id_district;
            item.IdDistrictCode = request.id_district_code;
            item.IdSubdistrict1 = request.id_subdistrict1;
            item.IdSubdistrict1Code = request.id_subdistrict1_code;
            item.IdSubdistrict2 = request.id_subdistrict2;
            item.IdSubdistrict2Code = request.id_subdistrict2_code;
            item.IdRt = request.id_rt;
            item.IdRw = request.id_rw;
            item.IdLat = request.id_lat;
            item.IdLng = request.id_lng;
            item.IdAddress = request.id_address;
            item.IdPostalCode = request.id_postal_code;
            item.DomOwnership = request.dom_ownership;
            item.DomProvince = request.dom_province;
            item.DomProvinceCode = request.dom_province_code;
            item.DomDistrict = request.dom_district;
            item.DomDistrictCode = request.dom_district_code;
            item.DomSubdistrict1 = request.dom_subdistrict1;
            item.DomSubdistrict1Code = request.dom_subdistrict1_code;
            item.DomSubdistrict2 = request.dom_subdistrict2;
            item.DomSubdistrict2Code = request.dom_subdistrict2_code;
            item.DomRt = request.dom_rt;
            item.DomRw = request.dom_rw;
            item.DomLat = request.dom_lat;
            item.DomLng = request.dom_lng;
            item.DomAddress = request.dom_address;
            item.DomPostalCode = request.dom_postal_code;
            item.IsExpressSurvey = request.is_express_survey;
            item.SurveyDate = request.survey_date;
            item.PlnNumber = request.PLN_Number;
            item.PamNumber = request.PAM_Number;
            item.BankName = request.bank_name;
            item.BankAccountNumber = request.bank_account_number;
            item.BankAccountName = request.bank_account_name;
            item.Source = request.source;
            item.MfinState = request.mfin_state;
            item.CreBy = request.cre_by;
            item.CreDate = DateTime.Now;
            item.CreIp = http.Request.Headers["REMOTE_ADDR"];
            item.ModBy = request.mod_by;
            item.ModDate = DateTime.Now;
            item.ModIp = http.Request.Headers["REMOTE_ADDR"];
            #endregion

            //List<string> listKey = new List<string>() { "PbbPhoto", "HousePhoto", "KtpPhoto", "SelfieWithKtpPhoto" };

            // Folder Upload
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            // Folder Dana Mobil
            uploadPath += separator + "DanaMobil";
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            // Folder entry
            uploadPath += separator + item.SiapId;
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            #region FileUpload
            var KtpPhoto = files.Where(a => a.Name == "ktp_photo").FirstOrDefault();
            if (KtpPhoto != null)
            {
                String[] arrSplit = KtpPhoto.FileName.Split(".");
                string type = "_KtpPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.KtpPhoto = filepath;

                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    KtpPhoto.CopyTo(fileStream);
                }
            }
            else
            {
                throw new Exception("KtpPhoto harus diuanggah!");
            }

            var SelfieWithKtpPhoto = files.Where(a => a.Name == "selfie_with_ktp_photo").FirstOrDefault();
            if (SelfieWithKtpPhoto != null)
            {
                String[] arrSplit = SelfieWithKtpPhoto.FileName.Split(".");
                string type = "_SelfieWithKtpPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.SelfieWithKtpPhoto = filepath;

                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    SelfieWithKtpPhoto.CopyTo(fileStream);
                }
            }
            else
            {
                throw new Exception("SelfieWithKtpPhoto harus diuanggah!");
            }

            var StnkPhoto = files.Where(a => a.Name == "stnk_photo").FirstOrDefault();
            if (StnkPhoto != null)
            {
                String[] arrSplit = StnkPhoto.FileName.Split(".");
                string type = "_StnkPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.StnkPhoto = filepath;

                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    StnkPhoto.CopyTo(fileStream);
                }
            }
            else
            {
                throw new Exception("StnkPhoto harus diuanggah!");
            }

            var CarPhoto = files.Where(a => a.Name == "car_photo").FirstOrDefault();
            if (CarPhoto != null)
            {
                String[] arrSplit = CarPhoto.FileName.Split(".");
                string type = "_CarPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.CarPhoto = filepath;

                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    CarPhoto.CopyTo(fileStream);
                }
            }
            else
            {
                throw new Exception("CarPhoto harus diuanggah!");
            }

            var npwpFile = files.Where(a => a.Name == "npwp_photo").FirstOrDefault();
            if (npwpFile != null)
            {
                String[] arrSplit = npwpFile.FileName.Split(".");
                string type = "_NpwpPhoto";
                String filename = item.NikNumber + "_" + item.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                String filepath = (uploadPath + separator + filename + ext);
                item.NpwpPhoto = filepath;

                using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                {
                    npwpFile.CopyTo(fileStream);
                }
            }
            #endregion

            db.SiapDms.Add(item);

            db.SaveChanges();

            return item.EntryId;
        }

        public static bool UpdateMfinStateDanaRumah(UpdateMfinState request, HttpContext http)
        {
            var siapdr = db.SiapDrs.Where(a => a.SiapId == request.siapID.Value).FirstOrDefault();

            if (siapdr != null) 
            {
                siapdr.MfinState = request.mfinState;
                siapdr.ModDate = DateTime.Now;
                siapdr.ModBy = "SIAP";
                siapdr.ModIp = http.Connection.RemoteIpAddress.ToString();

                if (request.mfinState == "credit-sign")
                {
                    var file = http.Request.Form.Files.Where(a => a.Name == "digisign_file").FirstOrDefault();
                    if (file != null)
                    {
                        String separator = "\\";

                        String uploadPath = hostingEnv.WebRootPath + separator + "Upload";

                        // Folder Upload
                        if (!System.IO.Directory.Exists(uploadPath))
                        {
                            System.IO.Directory.CreateDirectory(uploadPath);
                        }

                        // Folder Dana Rumah
                        uploadPath += separator + "DanaRumah";
                        if (!System.IO.Directory.Exists(uploadPath))
                        {
                            System.IO.Directory.CreateDirectory(uploadPath);
                        }

                        // Folder entry
                        uploadPath += separator + siapdr.SiapId;
                        if (!System.IO.Directory.Exists(uploadPath))
                        {
                            System.IO.Directory.CreateDirectory(uploadPath);
                        }

                        String[] arrSplit = file.FileName.Split(".");
                        string type = "_DigiSign";
                        String filename = siapdr.NikNumber + "_" + siapdr.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                        String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                        String filepath = (uploadPath + separator + filename + ext);
                        siapdr.DigisignFile = filepath;

                        using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                    else
                    {
                        throw new Exception("digisign_file harus diunggah!");
                    }
                }

                return db.SaveChanges() > 0;
            }
            else
            {
                throw new Exception("SiapID tidak ditemukan!");
            }
        }

        public static bool UpdateMfinStateDanaMobil(UpdateMfinState request, HttpContext http)
        {
            var siapdm = db.SiapDms.Where(a => a.SiapId == request.siapID.Value).FirstOrDefault();

            if (siapdm != null)
            {
                siapdm.MfinState = request.mfinState;
                siapdm.ModDate = DateTime.Now;
                siapdm.ModBy = "SIAP";
                siapdm.ModIp = http.Connection.RemoteIpAddress.ToString();

                if (request.mfinState == "credit-sign")
                {
                    var file = http.Request.Form.Files.Where(a => a.Name == "digisign_file").FirstOrDefault();
                    if (file != null)
                    {
                        String separator = "\\";

                        String uploadPath = hostingEnv.WebRootPath + separator + "Upload";

                        // Folder Upload
                        if (!System.IO.Directory.Exists(uploadPath))
                        {
                            System.IO.Directory.CreateDirectory(uploadPath);
                        }

                        // Folder Dana Mobil
                        uploadPath += separator + "DanaMobil";
                        if (!System.IO.Directory.Exists(uploadPath))
                        {
                            System.IO.Directory.CreateDirectory(uploadPath);
                        }

                        // Folder entry
                        uploadPath += separator + siapdm.SiapId;
                        if (!System.IO.Directory.Exists(uploadPath))
                        {
                            System.IO.Directory.CreateDirectory(uploadPath);
                        }

                        String[] arrSplit = file.FileName.Split(".");
                        string type = "_DigiSign";
                        String filename = siapdm.NikNumber + "_" + siapdm.SiapId + "_" + type + "_" + DateTime.Now.ToString("dd-MM-yyyyd_HHmmdd.ffff");
                        String ext = (arrSplit.Length > 1 ? "." + arrSplit.ElementAtOrDefault(arrSplit.Length - 1) : "");
                        String filepath = (uploadPath + separator + filename + ext);
                        siapdm.DigisignFile = filepath;

                        using (Stream fileStream = new FileStream(filepath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                    }
                    else
                    {
                        throw new Exception("digisign_file harus diunggah!");
                    }
                }

                return db.SaveChanges() > 0;
            }
            else
            {
                throw new Exception("SiapID tidak ditemukan!");
            }
        }
    }
}
