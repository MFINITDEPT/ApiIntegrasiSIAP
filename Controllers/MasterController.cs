using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiIntegrasiSIAP.Models.FACEDB;
using Microsoft.Extensions.Logging;
using ApiIntegrasiSIAP.Requests;
using ApiIntegrasiSIAP.Libs;
using Newtonsoft.Json;
using ApiIntegrasiSIAP.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using ApiIntegrasiSIAP.Models;

namespace ApiIntegrasiSIAP.Controllers
{
    public class MasterController : Controller
    {
        private FACEDBContext dBContext;
        private IConfiguration configs;
        private Config appconfig = new Config();

        public MasterController(FACEDBContext dBContext, IConfiguration config)
        {
            this.dBContext = dBContext;
            configs = config;
            config.Bind(appconfig);

            ExceptionHandlerUtils.appconfig = appconfig;

            Encrypt.key = appconfig.Encryption.key;
            Encrypt.iv = appconfig.Encryption.iv;
        }

        [HttpPost]
        public ActionResult Branch()
        {
            try {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<BranchRequest>(Encrypt.DecryptRJ256(bodycontent.Result));  

                var result = dBContext.MasterBranches.Where(x => x.AreaId == request.areaId).ToList();

                var response = new List<object>();
                if (result.Count > 0) {
                    foreach (var item in result)
                    {
                        var obj = new
                        {
                            item.BranchId,
                            item.City
                        };

                        response.Add(obj);
                    }
                }

               // return Content(JsonConvert.SerializeObject(result), "application/json");
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(response)));
            }
            catch (Exception ex) {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Area()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var result = dBContext.MasterAreas.ToList();

               // return Content(JsonConvert.SerializeObject(result), "application/json");
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(result)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Merk()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<MerkRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                DbUtils db = new DbUtils(dBContext);
                DataTable d = new DataTable();
                string query = "select distinct m.merk_code, m.description from MPriceListView mrp left join MMerkView m on m.merk_code = mrp.merk_code where mrp.branch = @branchId and mrp.status = @status";

                Dictionary<String, object> lsp = new Dictionary<string, object>();

                lsp.Add("@branchId", request.branchId);
                lsp.Add("@status", "AKTIF");

                d = db.GetDataTable(query, false, lsp);

               // return Content(JsonConvert.SerializeObject(d));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(d)));
            }
            catch (Exception ex) {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Model()
        {
            try {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<ModelRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                DbUtils db = new DbUtils(dBContext);
                DataTable d = new DataTable();
                string query = String.Format("SELECT distinct a.MODEL_CODE, [DESCRIPTION] from MModelView b left join MPriceListView a on a.MODEL_CODE = b.MODEL_CODE where b.MERK_CODE = @merkId and a.[STATUS] = @status");

                Dictionary<String, object> lsp = new Dictionary<string, object>();

                lsp.Add("@merkId", request.merkId);
                lsp.Add("@status", "AKTIF");

                d = db.GetDataTable(query, false, lsp);

                //return Content(JsonConvert.SerializeObject(d));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(d)));
            }
            catch (Exception ex) {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Type()
        {
            try {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<TypeRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                DbUtils db = new DbUtils(dBContext);
                DataTable d = new DataTable();
                string query = "select distinct a.type_code, description from MTypeView a left join MPriceListView b on a.TYPE_CODE = b.TYPE_CODE where a.model_code = @modelId and b.[STATUS] = @status";

                Dictionary<String, object> lsp = new Dictionary<string, object>();

                lsp.Add("@modelId", request.modelId);
                lsp.Add("@status", "AKTIF");

                d = db.GetDataTable(query, false, lsp);

               // return Content(JsonConvert.SerializeObject(d));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(d)));
            }
            catch (Exception ex) {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Year()
        {
            try {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<MRPRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                DbUtils db = new DbUtils(dBContext);
                DataTable d = new DataTable();
                string query = "select distinct ass_year from MPriceListView where merk_code = @merkId and model_code = @modelId and type_code = @typeId and status = @status";

                Dictionary<String, object> lsp = new Dictionary<string, object>();

                lsp.Add("@merkId", request.merkId);
                lsp.Add("@modelId", request.modelId);
                lsp.Add("@typeId", request.typeId);
                lsp.Add("@status", "AKTIF");

                d = db.GetDataTable(query, false, lsp);

               // return Content(JsonConvert.SerializeObject(d));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(d)));
            }
            catch (Exception ex) {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<object> PriceList()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<MRPRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                DbUtils db = new DbUtils(dBContext);
                DataTable d = new DataTable();
                string query = "select max_purchase 'max_loan' from MPriceListView where merk_code = @merkId and model_code = @modelId and type_code = @typeId and ass_year = @year and branch = @branchId and status = @status";

                Dictionary<String, object> lsp = new Dictionary<string, object>();

                lsp.Add("@merkId", request.merkId);
                lsp.Add("@modelId", request.modelId);
                lsp.Add("@typeId", request.typeId);
                lsp.Add("@year", request.year);
                lsp.Add("@branchId", request.branchId);
                lsp.Add("@status", "AKTIF");

                d = db.GetDataTable(query, false, lsp);

                if (d != null)
                {
                    var row = d.Rows[0];
                    object result = new
                    {
                        maxLoan = row["max_loan"]
                    };

                    //return Content(JsonConvert.SerializeObject(result));
                    return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(result)));
                }
                else
                {
                    return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 200, "Not Found");
                }
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }
           
        [HttpGet]
        public ActionResult Insurance() { 
         try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");
                var result = dBContext.MasterInsurances;
                List<object> finalResult =new List<object>();

                foreach (var item in result)
                {
                    finalResult.Add(item.Desc);
                }

                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(finalResult)));
            }  catch(Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Plat()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");
                var result = dBContext.MasterPlats;
                List<object> finalResult = new List<object>();

                foreach (var item in result)
                {
                    finalResult.Add(item.PlatCode);
                }

                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(finalResult)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpGet]
        public ActionResult KPRTenor()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");
                List<object> finalResult = new List<object>();

                for (int i = 1; i <= appconfig.tenorKPR; i++) {
                    var tenor = new {
                        Value = i * 12,
                        Tenor = $"{i * 12} Bulan"
                    };

                    finalResult.Add(tenor);
                }
              
              //  return Content(JsonConvert.SerializeObject(finalResult), "application/json");

                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(finalResult)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Blacklist()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");
                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<BlacklistRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                DbUtils db = new DbUtils(dBContext);
                DataTable d = new DataTable();
                string query = "EXEC [dbo].[CHECK_BLACKLIST] @INKTP";

                Dictionary<String, object> lsp = new Dictionary<string, object>();

                lsp.Add("@INKTP", request.ktp);

                d = db.GetDataTable(query, false, lsp);

                Blacklist result = new(); 

                if (d != null && d.Rows.Count > 0)
                {
                    var row = d.Rows[0];

                    result.KTP = row["KTP"].ToString();
                    result.STATUS = row["STATUS"].ToString();                    
                }
                else {
                    result.KTP = request.ktp;
                    result.STATUS = "NON BLACKLIST";                  
                }

                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(result)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }
    }
}
