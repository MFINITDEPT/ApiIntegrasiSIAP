using ApiIntegrasiSIAP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiIntegrasiSIAP.Libs;

namespace ApiIntegrasiSIAP.Exceptions
{
    public class ExceptionHandlerUtils
    {
        public static Config appconfig;

        public static ActionResult JsonOutput(Controller ctrl, HttpContext context,int statusCode, String msg) {
            context.Response.StatusCode = statusCode;
            var exception = new
            {
                reason = msg
            };

            if (appconfig.environment == "Prod")
            {
                return ctrl.Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(new
                {
                    statuscode = statusCode,
                    message = "Terjadi kesalahan. Silakan coba kembali"
                })), "application/json"); ;
            }
            else
            {
                return ctrl.Content(Encrypt.EncryptRJ256(JsonConvert.SerializeObject(new
                {
                    statuscode = statusCode,
                    message = msg
                })), "application/json");
            }
            //return new JsonResult(AES.EncryptRJ256(JsonConvert.SerializeObject(exception).ToString()));
        }
    }
}
