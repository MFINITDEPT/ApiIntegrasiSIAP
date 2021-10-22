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
using ApiIntegrasiSIAP.Models;
using System.Data;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ApiIntegrasiSIAP.Controllers
{
    public class SimulationController : Controller
    {

        private FACEDBContext _db;
        private ILogger<SimulationController> _logger;
        IConfiguration _config;
        Config appconfig = new Config();

        public SimulationController(ILogger<SimulationController> logger, FACEDBContext db, IConfiguration config)
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
        public ActionResult simulationKPR()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                SimulationRequest request = JsonConvert.DeserializeObject<SimulationRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                double hutang;
                double persenFee;
                int tenor;
                double bunga;
                double fee;

                var config = _db.Configurations.FirstOrDefault(xx => xx.ConfigurationName.ToLower() == "rate kpr");

                persenFee = double.Parse(StringUtils.replaceToKoma(_db.Configurations.FirstOrDefault(x => x.ConfigurationName.ToLower() == "%_Fee_KPR".ToLower()).ConfigurationValue));
                fee = (request.Pengajuan * (persenFee / 100));
                hutang = request.Pengajuan - fee;
                tenor = request.Tenor;

                try
                {
                    bunga = double.Parse(StringUtils.replaceToKoma(config.ConfigurationValue));
                }
                catch
                {
                    bunga = appconfig.defaultRate;
                }

                var bunga_bulan = (bunga / 12) / 100;
                var pembagi = 1 - (1 / Math.Pow(1 + bunga_bulan, tenor));
                var anuitas = hutang / (pembagi / bunga_bulan);

                var ang_bunga = hutang * ((bunga / 12) / 100);
                var ang_pokok = anuitas - ang_bunga;
                var cicilan = Math.Floor(ang_bunga + ang_pokok);

                var result = new
                {
                    Tenor = tenor,
                    EstInstallment = StringUtils.roundUpNDigitLeft(cicilan),
                    EstDisburstment = hutang,
                    fee
                };
              //  return Content(JsonConvert.SerializeObject(result));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(result)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult simulationOTO()
        {
            ///untuk ngambil parameter berupa string, dengan content type application/json
       
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                SimulationRequest request = JsonConvert.DeserializeObject<SimulationRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                var config = _db.Configurations.FirstOrDefault(xx => xx.ConfigurationName.ToLower() == "rate oto");
                double hutang;
                double bunga;
                double fee;

                double persenFee = double.Parse(StringUtils.replaceToKoma(_db.Configurations.FirstOrDefault(x => x.ConfigurationName.ToLower() == "%_Fee_OTO".ToLower()).ConfigurationValue));
                fee = (request.Pengajuan * (persenFee / 100));
                hutang = request.Pengajuan - fee;

                try
                {
                    bunga = double.Parse(StringUtils.replaceToKoma(config.ConfigurationValue));
                }
                catch
                {
                    bunga = 20;
                }

                List<object> result = new List<object>();
                for (int i = 0; i < appconfig.tenorOTO; i++)
                {
                    int tenor = i + 1;
                    var totalBunga = (hutang * (bunga / 100) * tenor);
                    var angsuran = Math.Floor((hutang + totalBunga) / (tenor * 12));
                    result.Add(new
                    {
                        tenor = tenor * 12,
                        estInstallment = StringUtils.roundUpNDigitLeft(angsuran),
                        estDisburstment = hutang
                    });
                }

                object finalresult = new
                {
                    fee,
                    installmentPlan = result
                };

               // return Content(JsonConvert.SerializeObject(finalresult));
               return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(finalresult)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SimulationET()
        {
            Data dtAR = new Data();
            DataTable dt = new DataTable();

            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");
               
                DbUtils db = new DbUtils(_db);
                //if (Oauth.IsValid() == "OK")
                //{
                    var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                    var obj = JsonConvert.DeserializeObject<SimulationETRequest>(Encrypt.DecryptRJ256(bodycontent.Result));

                    string ContractNO = obj.ContractNO;
                    string tanggal = Utils.convertDate2(obj.EtDate);
                    string query = "exec BKMDB.dbo.spExternalTerminationDraft @lsagree, @v_date, @otherfee";

                    Dictionary<String, Object> lsp = new Dictionary<string, object>();
                    lsp.Add("@lsagree", ContractNO);
                    lsp.Add("@v_date", tanggal);
                    lsp.Add("@otherfee", 0);

                    dt = db.GetDataTable(query, false, lsp);
                    dt.TableName = "AgreementET";

                    dtAR.data = dt;
                    dtAR.flag = 1;
                    dtAR.message = "Successfully";
                //}
                //else
                //{
                //    dtAR.flag = 0;
                //    dtAR.message = Oauth.IsValid();
                //}

              //  return Content(JsonConvert.SerializeObject(dtAR));
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(dtAR)));
            }
            catch (Exception ex)
            {
                dtAR.flag = 0;
                dtAR.message = "Failed. " + ex.Message.ToString();
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(dtAR)));
            }
        }
    }
}
