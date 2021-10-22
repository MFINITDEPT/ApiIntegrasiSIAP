using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiIntegrasiSIAP.Models
{
    public class Config
    {
        public Logging Logging { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Encryption Encryption { get; set; }
        public string AllowedHosts { get; set; }
        public Double defaultRate { get; set; }
        public Double tenorOTO { get; set; }
        public Double tenorKPR { get; set; }
        public String environment { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string Microsoft { get; set; }

        [JsonProperty("Microsoft.Hosting.Lifetime")]
        public string MicrosoftHostingLifetime { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class ConnectionStrings
    {
        public string FACEDB { get; set; }
        public string BKMDB { get; set; }
    }

    public class Encryption
    {
        public string iv { get; set; }
        public string key { get; set; }
    }
}
