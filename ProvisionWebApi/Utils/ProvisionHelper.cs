using ProvisionWebApi.Models;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace ProvisionWebApi.Utils
{
    public static class ProvisionHelper
    {
        public static ProvisionResponse GrantVBUIAccess(string env, string user)
        {
            ProvisionResponse result = new ProvisionResponse();
            try
            {
                //if (string.IsNullOrWhiteSpace(user)) user = Thread.CurrentPrincipal.Identity.Name;
                //else
                //{
                //    string emailAddress = ActiveDirectoryHelper.GetValue("mail", user);
                //}

                ActiveDirectoryHelper.ValidateUser(user);

                string response = SqlDataHelper.Provision(AppName.VBUI, env, user, string.Empty, Properties.Resources.Query_VBUIAcess);

                result.Status = "Success";
                result.Reason = "Access Granted";
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Reason = ex.Message;
            }
            return result;
        }

        public static ProvisionResponse GrantMOETAccess(string env, string user)
        {
            ProvisionResponse result = new ProvisionResponse();
            try
            {
                //if (string.IsNullOrWhiteSpace(user)) user = Thread.CurrentPrincipal.Identity.Name;
                //else
                //{
                //    string emailAddress = ActiveDirectoryHelper.GetValue("mail", user);
                //}

                ActiveDirectoryHelper.ValidateUser(user);

                string response = SqlDataHelper.Provision(AppName.MOET, env, user, string.Empty, Properties.Resources.Query_MOETAccess);

                if (response == "1")
                {
                    result.Status = "Success";
                    result.Reason = "Access Granted";
                }
                else
                {
                    result.Status = "Error";
                    result.Reason = "Unable to grant MOET Access.";
                }
            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Reason = ex.Message;
            }
            return result;
        }

        public static ProvisionResponse GrantMSLAccess(string env, string user, string pcn, bool needPQRole)
        {
            ProvisionResponse result = new ProvisionResponse();
            try
            {
                //if (string.IsNullOrWhiteSpace(user)) user = Thread.CurrentPrincipal.Identity.Name;

                ActiveDirectoryHelper.ValidateUser(user);

                string response = SqlDataHelper.Provision(AppName.MSL, env, user, pcn, (needPQRole ? Properties.Resources.Query_MSL_PQ : Properties.Resources.Query_MSLAccess));

                using (XmlReader reader = XmlReader.Create(new StringReader(response)))
                {
                    XElement root = XElement.Load(reader);
                    XmlNameTable nameTable = reader.NameTable;
                    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
                    namespaceManager.AddNamespace("ns1", "http://MS.IT.Ops.MSLicense.TransactionService.DataContracts/");

                    XElement element = root.XPathSelectElement("/ns1:Accessor/ns1:AccessorStatus/ns1:StatusID", namespaceManager);

                    if (element == null || element.Value != "1")
                    {
                        result.Status = "Error";
                    }
                    else
                    {
                        result.Status = "Success";
                        result.Reason = "Access Granted";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Reason = ex.Message;
            }
            return result;
        }

        public static ProvisionResponse GrantMsQuoteAccess(string env, string user, string mq1 = "D", string mq2 = "D", string mq3 = "D", string mq4 = "D", string qsu = "D", string mqf = "D", string qv = "D")
        {
            ProvisionResponse result = new ProvisionResponse();
            try
            {
                //if (string.IsNullOrWhiteSpace(user)) user = Thread.CurrentPrincipal.Identity.Name;

                ActiveDirectoryHelper.ValidateUser(user);

                bool response = SqlDataHelper.ProvisionMsQuote(env, Properties.Resources.Query_MSQuote, user, mq1, mq2, mq3, mq4, qsu, mqf, qv);

                if (!response)
                {
                    result.Status = "Success";
                    result.Reason = "Access Revoked";
                }
                else
                {
                    result.Status = "Success";
                    result.Reason = "Access Granted";
                }

            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Reason = ex.Message;
            }
            return result;
        }

        public static ProvisionResponse GrantMopetAccess(string env, string user)
        {
            ProvisionResponse result = new ProvisionResponse();
            try
            {
                //if (string.IsNullOrWhiteSpace(user)) user = Thread.CurrentPrincipal.Identity.Name;

                ActiveDirectoryHelper.ValidateUser(user);

                bool response = SqlDataHelper.ProvisionMopet(env, Properties.Resources.Query_MOPET, user);

                if (!response)
                {
                    result.Status = "Success";
                    result.Reason = "Access Revoked";
                }
                else
                {
                    result.Status = "Success";
                    result.Reason = "Access Granted";
                }

            }
            catch (Exception ex)
            {
                result.Status = "Error";
                result.Reason = ex.Message;
            }
            return result;
        }
    }
}
