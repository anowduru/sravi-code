using ProvisionWebApi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace ProvisionWebApi.Controllers
{
    public class EnvironmentController : ApiController
    {
        public EnvironmentDataModel[] Get()
        {
            List<EnvironmentDataModel> items = new List<EnvironmentDataModel>();

            foreach (ConnectionStringSettings conn in ConfigurationManager.ConnectionStrings)
            {
                if (conn.Name.StartsWith("Local", StringComparison.OrdinalIgnoreCase) || conn.Name.ToUpper().Contains("MOET") || conn.Name.ToUpper().Contains("MOPET")) continue;
                var envItem = new EnvironmentDataModel
                {
                    Name = conn.Name,
                    hasMoet = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().FirstOrDefault(item => item.Name.Contains(conn.Name + "_MOET")) != null,
                    hasMopet = ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().FirstOrDefault(item => item.Name.Contains(conn.Name + "_MOPET")) != null
                };

                items.Add(envItem);

            }
            return items.ToArray();
        }
    }
}
