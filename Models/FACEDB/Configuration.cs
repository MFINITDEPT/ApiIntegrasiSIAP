using System;
using System.Collections.Generic;

#nullable disable

namespace ApiIntegrasiSIAP.Models.FACEDB
{
    public partial class Configuration
    {
        public Guid ConfigurationId { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigurationValue { get; set; }
    }
}
