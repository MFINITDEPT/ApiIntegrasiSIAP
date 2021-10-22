using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApiIntegrasiSIAP.Libs
{
    public class StringUtils
    {
        static public double roundUpNDigitLeft(double number, int nDigit = 3) {
            String strings = number.ToString();
            String zeroRightPad = "";

            for (int i = 0; i < nDigit; i++)
            {
                zeroRightPad += "0";
            }

            String result = "";
            if (strings.Length <= nDigit)
            {
                result = "1"+zeroRightPad;
            }
            else
            {
                var temp = strings.Substring(0, strings.Length - nDigit);
                var numbers = int.Parse(temp) + 1;
                result =  numbers + zeroRightPad;
            }
            return double.Parse(result);
        }

        public static string getBodyRaw(HttpContext context)
        {
            StreamReader bodyStream = new StreamReader(stream: context.Request.Body);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            return bodyText;
        }

        public static string replaceToKoma(string value)
        {
            return value.Replace(".",",");
        }

    }
}
