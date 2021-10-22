using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Models
{
    public class UpdateMfinState
    {
        public string mfinState { get; set; }
        public int? siapID { get; set; }

        public Dictionary<string, string> Validate(HttpContext http)
        {
            Dictionary<String, String> validationErrors = new Dictionary<string, string>();

            if (String.IsNullOrEmpty(mfinState))
            {
                validationErrors.Add("mfinState", "mfinState harus diisi!");
            }

            if (!siapID.HasValue)
            {
                validationErrors.Add("siapID", "siapID harus diisi!");
            }

            if (mfinState == "credit-sign")
            {
                var files = http.Request.Form.Files;

                if (files == null || files.Count() <= 0)
                {
                    validationErrors.Add("digisign_file", "digisign_file harus diunggah!");
                }
                else
                {
                    var file = files.Where(a => a.Name == "digisign_file").FirstOrDefault();
                    if (file == null)
                    {
                        validationErrors.Add("digisign_file", "digisign_file harus diunggah!");
                    }
                }
            }

            return validationErrors;
        }
    }
}
