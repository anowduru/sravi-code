using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProvisionWebApi.Models
{
    public class EnvironmentDataModel
    {
        public string Name { get; set; }

        public bool hasMoet { get; set; }

        public bool hasMopet { get; set; }
    }
}