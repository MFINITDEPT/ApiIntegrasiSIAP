using ApiIntegrasiSIAP.Exceptions;
using ApiIntegrasiSIAP.Libs;
using ApiIntegrasiSIAP.Models;
using ApiIntegrasiSIAP.Models.FACEDB;
using ApiIntegrasiSIAP.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace ApiIntegrasiSIAP.Controllers
{
    public class ContractController : Controller
    {

        private FACEDBContext _db;
        private ILogger<ContractController> _logger;
        private IConfiguration _config;
        private Config appconfig = new Config();

        public ContractController(ILogger<ContractController> logger, FACEDBContext db, IConfiguration config)
        {
            _logger = logger;
            _db = db;
            _config = config;
            _config.Bind(appconfig);

            ExceptionHandlerUtils.appconfig = appconfig;

            Encrypt.key = appconfig.Encryption.key;
            Encrypt.iv = appconfig.Encryption.iv;
        }

        [HttpPost]
        public IActionResult Enquiry()
        {
            AgreeByNIK abn = new AgreeByNIK();

            try
            {
                 if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                DbUtils db = new DbUtils(_db);
                //if (Oauth.IsValid() == "OK")
                //{
                    var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                    var obj = JsonConvert.DeserializeObject<AgreeInfoByNik>(Encrypt.DecryptRJ256(bodycontent.Result));

                    string nik = obj.nik;

                    DataSet ds = new DataSet();
                    List<DataNIK> ldn = new List<DataNIK>();

                    string query = "exec BKMDB.dbo.getDataByNIK_BK @ktp";

                    Dictionary<String, object> lsp = new Dictionary<string, object>();

                    lsp.Add("@ktp", nik);
                    
                    ds = db.GetDataSet(query, false, lsp);

                    if (ds.Tables[0].Rows[0]["LESSEE"].ToString() != "")
                    {
                        abn.nik = ds.Tables[0].Rows[0]["NIK"].ToString();
                        abn.lessee = ds.Tables[0].Rows[0]["LESSEE"].ToString();
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            DataNIK dn = new DataNIK();
                            dn.lsagree = row["LSAGREE"].ToString();
                            dn.vitual = row["VIRTUAL_ACCNO"].ToString();
                            dn.jt_tempo = row["LAST_DUEDATE"].ToString();

                            ldn.Add(dn);
                        }
                        abn.datanik = ldn.ToArray();
                        abn.flag = 1;
                        abn.message = "Successfully";
                    }
                    else
                    {
                        abn.flag = 0;
                        abn.message = "Client with " + nik + " not Found at MNC Finance.";
                    }
                //}
                //else
                //{
                //    abn.flag = 0;
                //    abn.message = Oauth.IsValid();
                //}

               // return Content(JsonConvert.SerializeObject(abn));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(abn)));
            }
            catch (Exception e)
            {
                abn.flag = 0;
                abn.message = "Failed. " + e.Message.ToString();
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(abn)));
            }
        }

        [HttpPost]
        public IActionResult AgreementInfo()
        {
            AgreementInfo AI = new AgreementInfo();
            DataTable ds = new DataTable();

            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                DbUtils db = new DbUtils(_db);

                //if (Oauth.IsValid() == "OK")
                //{
                    var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                    var obj = JsonConvert.DeserializeObject<FrmInstallmentScheduleEnquiry>(Encrypt.DecryptRJ256(bodycontent.Result));
                    string ContractNO = obj.ContractNO;

                    //                    string query = @"select ls.LSAGREE [NoKontrak],ls.NAME [Nama] ,sc.C_NAME [namaCabang],ls.OUTSTANDING_PRINCIPAL, ls.OUTSTANDING_AR,v.POLICENO [NoPolisi],v.DESCRIPTION [AssetDescription],ls.OS_PERIOD [sisaTenor],ls.VIRTUAL_ACCNO [virtualAccountNo],day(ls.LAST_DUEDATE) TglAngsuran, ls.OVERDUE AS OVD, ls.RENTAL as [Cicilan], convert(varchar(10),ls.LAST_DUEDATE,120) TGL_JT_TEMPO from MSIX.dbo.LS_AGREEMENT ls
                    //                                        --inner join MSIX.dbo.LS_AGREEASSET a on a.LSAGREE=ls.LSAGREE
                    //                                        inner join MSIX.dbo.LS_ASSETVEHICLE v on v.LSAGREE=ls.LSAGREE
                    //                                        inner join MSIX.dbo.SYS_COMPANY sc on ls.C_CODE=sc.C_CODE
                    //                                        where ls.LSAGREE='" + ContractNO + "'";
                    string query = "exec BKMDB.dbo.getDataAgreementInfo @p_LSAGREE";

                    Dictionary<String, object> lsp = new Dictionary<string, object>();

                    lsp.Add("@p_LSAGREE", ContractNO);

                    ds = db.GetDataTable(query, false, lsp);
                    ds.TableName = "Agreementinfo";

                    AI.data = ds;
                    AI.flag = 1;
                    AI.message = "Successfully";
                //}
                //else
                //{
                //    AI.flag = 0;
                //    AI.message = Oauth.IsValid();
                //}

                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(AI)));
            }
            catch (Exception e)
            {
                AI.flag = 0;
                AI.message = "Failed. " + e.Message;
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(AI)));
            }
        }

        [HttpPost]
        public IActionResult InstallmentSchedule()
        {
            DataARSchedule dtAR = new DataARSchedule();
            DataSet ds = new DataSet();

            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                DbUtils db = new DbUtils(_db);

                //if (Oauth.IsValid(HttpContext) == "OK")
                //{
                    //var obj = JsonConvert.DeserializeObject<FrmInstallmentScheduleEnquiry>(Encrypt.DecryptRJ256(Utils.getBodyRaw(HttpContext)));
                 //   string ContractNO = "02519251100017"; //obj.ContractNO;
                    var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                    var obj = JsonConvert.DeserializeObject<FrmInstallmentScheduleEnquiry>(Encrypt.DecryptRJ256(bodycontent.Result));
                    string ContractNO = obj.ContractNO;

                    string query = "EXEC BKMDB.dbo.getKartuAR @p_lsagree";

                    Dictionary<String, Object> lsp = new Dictionary<string, object>();
                    lsp.Add("@p_lsagree", ContractNO);

                    ds = db.GetDataSet(query, false, lsp);

                    ds.Tables[1].TableName = "kartuAR";
                    dtAR.TanggalMulai = ds.Tables[0].Rows[0]["TanggalMulai"].ToString();
                    dtAR.TotalHutang = ds.Tables[0].Rows[0]["TotalHutang"].ToString().Replace(",", ".");
                    dtAR.TipeUnit = ds.Tables[0].Rows[0]["TipeUnit"].ToString();
                    dtAR.Rental = ds.Tables[0].Rows[0]["RENTAL"].ToString();
                    dtAR.data = ds.Tables[1];
                    dtAR.flag = 1;
                    dtAR.message = "Successfully";
                //}
                //else
                //{
                //    dtAR.flag = 0;
                //    dtAR.message = Oauth.IsValid(HttpContext);
                //}

                //return Content(JsonConvert.SerializeObject(dtAR).ToString(), "application/json");
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(dtAR)));
            }
            catch (Exception e)
            {

                dtAR.flag = 0;
                dtAR.message = "Failed. " + e.Message;
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(dtAR)));
            }
        }

        [HttpPost]
        public IActionResult getContractFile()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                AgreementDocument FilePdf = new AgreementDocument();

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var obj = JsonConvert.DeserializeObject<AgreementFileRequest>(Encrypt.DecryptRJ256(bodycontent.Result));   
           
                string ContractNo = obj.ContractNo;
                string Doc = obj.Doc;
                DbUtils db = new DbUtils(_db);
                // if (Oauth.IsValid() == "OK")
                // {
                    string qry = "";
                    string filename = "";
                    string pdf_loc = "";
                    string pdf_name = "";
                    if (Doc == "Dokumen Pelanggan")
                    {
                        filename = $"InformasiPenting_{ContractNo}_{Doc}";
                        pdf_name = $"{filename}.pdf";
                    //alamat aslinya ini
                    //pdf_loc = @"E:\application\WebServiceBKM\WebServiceBKM\folder\" + pdf_name;
                    pdf_loc = @"D:\application\WebServiceBKM\WebServiceBKM\folder\" + pdf_name;
                    if (!System.IO.File.Exists(pdf_loc))
                        {
                            CreatePDF.generatePDFfile(db, ContractNo, pdf_loc);
                        }
                    }
                    else
                    {
                        qry = "exec BKMDB.dbo.GetFileDocumentBK_byDoc @No_Contract, @Doc";
                        Dictionary<String, Object> lsp = new Dictionary<string, object>();
                        lsp.Add("@No_Contract", ContractNo);
                        lsp.Add("@Doc", Doc);

                        DataTable dt = db.GetDataTable(qry, false, lsp);
                        if (dt.Rows.Count > 0)
                        {
                            pdf_loc = dt.Rows[0]["FILEPATH"].ToString();
                        }
                    }

                    if (System.IO.File.Exists(pdf_loc))
                    {
                        FilePdf.filename = ContractNo;
                        FilePdf.flag = 1;
                        FilePdf.Message = "Successfully";
                        BinaryReader binReader = new BinaryReader(System.IO.File.Open(pdf_loc, FileMode.Open, FileAccess.Read));
                        binReader.BaseStream.Position = 0;
                        byte[] binFile = binReader.ReadBytes(Convert.ToInt32(binReader.BaseStream.Length));
                        binReader.Close();
                        FilePdf.data = binFile;
                    }
                    else
                    {
                        FilePdf.filename = ContractNo;
                        FilePdf.flag = 0;
                        FilePdf.Message = "File PDF not Found!!!";
                    }
                //}
                // else {
                //     FilePdf.filename = ContractNo;
                //     FilePdf.flag = 0;
                //     FilePdf.Message = Oauth.IsValid();
                // }
                // return Content(JsonConvert.SerializeObject(FilePdf));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(FilePdf)));
            }
            catch (Exception e)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, e.Message);
            }
        }
    }
}
