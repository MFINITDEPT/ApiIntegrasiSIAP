using ApiIntegrasiSIAP.Exceptions;
using ApiIntegrasiSIAP.Libs;
using ApiIntegrasiSIAP.Models;
using ApiIntegrasiSIAP.Models.FACEDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Controllers
{
    public class OwnerController : Controller
    {
        private FACEDBContext _db;
        private readonly ILogger<OwnerController> _logger;
        private readonly IConfiguration _config;
        private Config appconfig = new Config();

        public OwnerController(ILogger<OwnerController> logger, IConfiguration config, FACEDBContext dBContext)
        {
            _db = dBContext;
            _logger = logger;
            _config = config;
            _config.Bind(appconfig);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");
            return this.GetAll();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            if (!Oauth.IsValid(HttpContext)) return ExceptionHandlerUtils.JsonOutput(this, HttpContext, 401, "UnAuthorized");
            int statusCode = 200;
            try
            {
                var listOwnership = _db.MasterOwnerships.ToList();

                if (listOwnership != null && listOwnership.Count() > 0)
                {
                    HttpContext.Response.StatusCode = statusCode;

                    return Json(new
                    {
                        status = statusCode,
                        message = "OK",
                        data = listOwnership
                    });
                }
                else
                {
                    statusCode = 404;
                    HttpContext.Response.StatusCode = statusCode;

                    return Json(new
                    {
                        status = statusCode,
                        message = "Not Found",
                        data = listOwnership
                    });
                }
            }
            catch (Exception)
            {
                statusCode = 500;
                HttpContext.Response.StatusCode = statusCode;

                return Json(new
                {
                    status = statusCode,
                    message = "Terjadi kesalahan saat mengambil data!",
                    data = new List<object>() { }
                });
            }
        }
    }
}
