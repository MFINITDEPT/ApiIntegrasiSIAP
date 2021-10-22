using ApiIntegrasiSIAP.Exceptions;
using ApiIntegrasiSIAP.Libs;
using ApiIntegrasiSIAP.Models;
using ApiIntegrasiSIAP.Models.FACEDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Controllers
{
    public class ProcessController : Controller
    {
        private FACEDBContext _db;
        private ILogger<ProcessController> _logger;
        IConfiguration _config;
        Config appconfig = new Config();

        public ProcessController(ILogger<ProcessController> logger, FACEDBContext db, IConfiguration config)
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
        public ActionResult SubmitSIAPDanaRumah()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<SiapDr>(Encrypt.DecryptRJ256(bodycontent.Result));
   

                _db.SiapDrs.Add(request);
                _db.SaveChanges();

                var result = _db.SiapDrs.FirstOrDefault(e => e == request);
               
                var response = new { 
                    status = "Success",
                    result.EntryId
                };

               // return Content(JsonConvert.SerializeObject(response), "application/json");
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(response)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SubmitSIAPDanaMobil()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<SiapDm>(Encrypt.DecryptRJ256(bodycontent.Result));

                _db.SiapDms.Add(request);
                _db.SaveChanges();

                var result = _db.SiapDms.FirstOrDefault(e => e == request);

                var response = new
                {
                    status = "Success",
                    result.EntryId
                };

               // return Content(JsonConvert.SerializeObject(response), "application/json");
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(response)));
            }
            catch (Exception ex)
            {
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 404, ex.Message);
            }
        }

    }
}
