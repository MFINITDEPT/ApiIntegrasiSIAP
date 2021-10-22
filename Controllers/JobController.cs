﻿using ApiIntegrasiSIAP.Exceptions;
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
    public class JobController : Controller
    {
        private FACEDBContext _db;
        private readonly ILogger<JobController> _logger;
        private readonly IConfiguration _config;
        private Config appconfig = new Config();

        public JobController(ILogger<JobController> logger, IConfiguration config, FACEDBContext dBContext)
        {
            _db = dBContext;
            _logger = logger;
            _config = config;
            _config.Bind(appconfig);

            ExceptionHandlerUtils.appconfig = appconfig;
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
                var listJobs = _db.MasterJobs.ToList();

                if (listJobs != null && listJobs.Count() > 0)
                {
                    HttpContext.Response.StatusCode = statusCode;

                    return Json(new
                    {
                        status = statusCode,
                        message = "OK",
                        data = listJobs
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
                        data = listJobs
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
