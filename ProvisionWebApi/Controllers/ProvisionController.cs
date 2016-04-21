using ProvisionWebApi.Models;
using ProvisionWebApi.Utils;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace ProvisionWebApi.Controllers
{
    public class ProvisionController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public JsonResult<ProvisionResponse> Post([FromBody]PermissionRequest data)
        {
            ProvisionResponse provisionResponse = new ProvisionResponse();

            try
            {
                string env = data.Environment;
                string app = data.Application;

                //no user name validation happens. For ex. whether the user exist in Active Directory or not
                var info = new StringBuilder();
                var log = new StringBuilder();

                if (app == "VBUI")
                {
                    foreach (var userName in (data.Users ?? "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        provisionResponse = ProvisionHelper.GrantVBUIAccess(env, userName.Trim());

                        if (provisionResponse.Status != "Success")
                        {
                            log.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                        else
                        {
                            info.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                    }
                }
                else if (app == "MOET")
                {
                    foreach (var userName in (data.Users ?? "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        provisionResponse = ProvisionHelper.GrantMOETAccess(env, userName.Trim());

                        if (provisionResponse.Status != "Success")
                        {
                            log.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                        else
                        {
                            info.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                    }
                }
                else if (app == "eMSL")
                {
                    foreach (var userName in (data.Users ?? "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        provisionResponse = ProvisionHelper.GrantMSLAccess(env, userName.Trim(), data.PCN.Trim(), data.PQAccess);

                        if (provisionResponse.Status != "Success")
                        {
                            log.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                        else
                        {
                            info.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                    }
                }
                else if (app == "MSQuote")
                {
                    foreach (var userName in (data.Users ?? "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        provisionResponse = ProvisionHelper.GrantMsQuoteAccess(env, userName.Trim(),
                                                              data.MSQRoles.First(i => i.Name == "MQ1").Value,
                                                              data.MSQRoles.First(i => i.Name == "MQ2").Value,
                                                              data.MSQRoles.First(i => i.Name == "MQ3").Value,
                                                              data.MSQRoles.First(i => i.Name == "MQ4").Value,
                                                              data.MSQRoles.First(i => i.Name == "QSU").Value,
                                                              data.MSQRoles.First(i => i.Name == "MQF").Value,
                                                              data.MSQRoles.First(i => i.Name == "QV").Value);
                        if (provisionResponse.Status != "Success")
                        {
                            log.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                        else
                        {
                            info.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                    }
                }
                else if (app == "MOPET")
                {
                    foreach (var userName in (data.Users ?? "").Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
                    {
                        provisionResponse = ProvisionHelper.GrantMopetAccess(env, userName.Trim());
                        if (provisionResponse.Status != "Success")
                        {
                            log.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                        else
                        {
                            info.AppendFormat("User: {0}<br />Status:{1}<br />Reason: {2}", userName, provisionResponse.Status, provisionResponse.Reason);
                        }
                    }
                }
                else
                {
                    provisionResponse.Status = "Error";
                    provisionResponse.Message1 = "Selected application is not supported.";
                }

                if (info.Length > 0)
                {
                    provisionResponse.Message1 = info.ToString();
                }

                if (log.Length > 0)
                {
                    provisionResponse.Message2 = log.ToString();
                }


            }
            catch (Exception ex)
            {
                provisionResponse.Status = "Error";
                provisionResponse.Message1 = ex.Message;
            }

            return Json<ProvisionResponse>(provisionResponse);
        }
    }
}
