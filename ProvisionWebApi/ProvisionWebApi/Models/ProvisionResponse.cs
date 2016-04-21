using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvisionWebApi.Models
{
    public class ProvisionResponse
    {
        public string Status { get; set; }

        public string Reason { get; set; }

        public string Message1 { get; set; }

        public string Message2 { get; set; }
    }
}