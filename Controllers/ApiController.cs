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
using ApiIntegrasiSIAP.Repositories;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace ApiIntegrasiSIAP.Controllers
{
    public class ApiController : Controller
    {
        private FACEDBContext _db;
        private ILogger<ApiController> _logger;
        IConfiguration _config;
        Config appconfig = new Config();
        IWebHostEnvironment _hostingEnvironment;

        public ApiController(ILogger<ApiController> logger, FACEDBContext db, IWebHostEnvironment hostingEnvironment, IConfiguration config)
        {
            _logger = logger;
            _db = db;
            _config = config;

            _config.Bind(appconfig);

            _hostingEnvironment = hostingEnvironment;

            SiapRepository.db = _db;
            SiapRepository.hostingEnv = _hostingEnvironment;

            ExceptionHandlerUtils.appconfig = appconfig;

            Encrypt.key = appconfig.Encryption.key;
            Encrypt.iv = appconfig.Encryption.iv;
        }

        [HttpPost]
        public ActionResult DanaRumah()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = HttpContext.Request.Form["bodyJson"]; //Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<UploadDanaRumah>(Encrypt.DecryptRJ256(bodycontent));

                var validationError = request.Validate(HttpContext);

                if(validationError != null && validationError.Count() > 0)
                {
                    HttpContext.Response.StatusCode = 422;
                    return Json(new
                    {
                        status = 422,
                        message = "Validation Error",
                        validationErrors = validationError.Select(a => new {
                            a.Key,
                            a.Value
                        }).ToList()
                    });
                }

                var result = SiapRepository.CreateDanaRumah(request, HttpContext);

                var response = new
                {
                    status = "Success",
                    EntryId = result
                };

                // return Content(JsonConvert.SerializeObject(response), "application/json");
                HttpContext.Response.StatusCode = 201;
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(response)), "application/json");
            }
            catch (Exception ex)
            {
                Utils.WriteLog(_hostingEnvironment, HttpContext, ex, null);

                ExceptionHandlerUtils.appconfig = appconfig;
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 500, ex.Message);
            }
        }

        [HttpPut]
        [ActionName("DanaRumah")]
        public ActionResult UpdateDanaRumah()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = HttpContext.Request.Form["bodyJson"]; //Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<UpdateMfinState>(Encrypt.DecryptRJ256(bodycontent));

                var validationError = request.Validate(HttpContext);

                if (validationError != null && validationError.Count() > 0)
                {
                    HttpContext.Response.StatusCode = 422;
                    return Json(new
                    {
                        status = 422,
                        message = "Validation Error",
                        validationErrors = validationError.Select(a => new {
                            a.Key,
                            a.Value
                        }).ToList()
                    });
                }

                var result = SiapRepository.UpdateMfinStateDanaRumah(request, HttpContext);

                var response = new
                {
                    status = "Success",
                    EntryId = result
                };

                // return Content(JsonConvert.SerializeObject(response), "application/json");
                HttpContext.Response.StatusCode = 200;
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(response)), "application/json");
            }
            catch (Exception ex)
            {
                Utils.WriteLog(_hostingEnvironment, HttpContext, ex, null);

                ExceptionHandlerUtils.appconfig = appconfig;
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DanaMobil()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = HttpContext.Request.Form["bodyJson"]; //Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<UploadDanaMobil>(Encrypt.DecryptRJ256(bodycontent));

                var validationError = request.Validate(HttpContext);

                if (validationError != null && validationError.Count() > 0)
                {
                    HttpContext.Response.StatusCode = 422;
                    return Json(new
                    {
                        status = 422,
                        message = "Validation Error",
                        validationErrors = validationError.Select(a => new {
                            a.Key,
                            a.Value
                        }).ToList()
                    });
                }

                var result = SiapRepository.CreateDanaMobil(request, HttpContext);

                var response = new
                {
                    status = "Success",
                    EntryId = result
                };

                // return Content(JsonConvert.SerializeObject(response), "application/json");
                HttpContext.Response.StatusCode = 201;
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(response)), "application/json");
            }
            catch (Exception ex)
            {
                Utils.WriteLog(_hostingEnvironment, HttpContext, ex, null);

                ExceptionHandlerUtils.appconfig = appconfig;
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 500, ex.Message);
            }
        }

        [HttpPut]
        [ActionName("DanaMobil")]
        public ActionResult UpdateDanaMobil()
        {
            try
            {
                if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");

                var bodycontent = HttpContext.Request.Form["bodyJson"]; //Utils.getBodyRawAsync(HttpContext.Request);
                var request = JsonConvert.DeserializeObject<UpdateMfinState>(Encrypt.DecryptRJ256(bodycontent));

                var validationError = request.Validate(HttpContext);

                if (validationError != null && validationError.Count() > 0)
                {
                    HttpContext.Response.StatusCode = 422;
                    return Json(new
                    {
                        status = 422,
                        message = "Validation Error",
                        validationErrors = validationError.Select(a => new {
                            a.Key,
                            a.Value
                        }).ToList()
                    });
                }

                var result = SiapRepository.UpdateMfinStateDanaMobil(request, HttpContext);

                var response = new
                {
                    status = "Success",
                    EntryId = result
                };

                // return Content(JsonConvert.SerializeObject(response), "application/json");
                HttpContext.Response.StatusCode = 200;
                return Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(response)), "application/json");
            }
            catch (Exception ex)
            {
                Utils.WriteLog(_hostingEnvironment, HttpContext, ex, null);

                ExceptionHandlerUtils.appconfig = appconfig;
                return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 500, ex.Message);
            }
        }

        //[HttpPost]
        //public IActionResult TestEncrypt()
        //{
        //    String text = HttpContext.Request.Form["text"];
        //    return Content(Encrypt.EncryptRJ256(text));
        //}

        //[HttpPost]
        //public IActionResult TestDecrypt()
        //{
        //    String text = HttpContext.Request.Form["text"];
        //    return Content(Encrypt.DecryptRJ256(text));
        //}
    }
}
