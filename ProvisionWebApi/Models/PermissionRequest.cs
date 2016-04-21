using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ProvisionWebApi.Models
{
    public class PermissionRequest
    {
        [DataMember]
        public string Environment { get; set; }

        [DataMember]
        public string Application { get; set; }

        [DataMember]
        public string Users { get; set; }

        [DataMember]
        public bool PQAccess { get; set; }

        [DataMember]
        public string PCN { get; set; }

        [DataMember]
        public QuoteRole[] MSQRoles { get; set; }
    }
}