using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Libs
{
    public class Utils
    {
        public static async Task<string> getBodyRawAsync(HttpRequest http)
        {
            http.EnableBuffering();
            Stream body = http.Body;
            body.Position = 0;
            StreamReader bodyStream = new StreamReader(body);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            String bodyText = await bodyStream.ReadToEndAsync();
            return bodyText;
        }

        public static string convertDate2(string str)
        {
            string hasil = "29/01/2013";
            hasil = str.Substring(6, 4) + "-" + str.Substring(3, 2) + "-" + str.Substring(0, 2);
            return hasil;
        }

        public static void WriteLog(IWebHostEnvironment hostingEnvironment, HttpContext http, Exception ex, string requestBody)
        {
            String _rootdir = hostingEnvironment.ContentRootPath;
            String separator = (OperatingSystem.IsWindows() ? "\\" : "/");

            if (!System.IO.Directory.Exists(_rootdir + separator + "Logs"))
            {
                System.IO.Directory.CreateDirectory(_rootdir + separator + "Logs");
            }

            String datenow = DateTime.Now.ToString("dd-MM-yyyy");
            String filename = _rootdir + separator + "Logs" + separator + "Logs_" + datenow + ".log";

            using (StreamWriter sw = System.IO.File.AppendText(filename))
            {
                sw.WriteLine("Begin Log");
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff"));
                sw.WriteLine(http.Connection.RemoteIpAddress != null ? http.Connection.RemoteIpAddress.ToString() : null);
                sw.WriteLine(String.Empty);
                sw.WriteLine("Message:");
                sw.WriteLine(ex.Message);
                sw.WriteLine("Stack Trace:");
                sw.WriteLine(ex.StackTrace);
                if (ex.InnerException != null) {
                    sw.WriteLine("Inner Exception Message:");
                    sw.WriteLine(ex.InnerException.Message);
                    sw.WriteLine("Inner Exception Stack Trace:");
                    sw.WriteLine(ex.InnerException.StackTrace);
                }
                sw.WriteLine("Request:");
                sw.WriteLine("Header");
                sw.WriteLine(JsonConvert.SerializeObject(http.Request.Headers.ToList()));
                var contentType = http.Request.Headers.Where(a => a.Key == "Content-Type").FirstOrDefault();
                if (contentType.GetType() is null && contentType.Value.Contains("multipart/form-data"))
                {
                    sw.WriteLine("Form");
                    sw.WriteLine(JsonConvert.SerializeObject(http.Request.Form.ToList()));
                }
                sw.WriteLine("Body");
                sw.WriteLine(requestBody);
                sw.WriteLine(String.Empty);
                sw.WriteLine("End Log");
                sw.WriteLine("");
                sw.WriteLine("");
            }
        }
    }
}
