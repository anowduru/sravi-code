using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ProvisionWebApi.Utils
{
    public enum AppName
    {
        VBUI,
        MSL,
        MOET,
        MSQuote,
        MOPET
    }

    public static class SqlDataHelper
    {
        public static string Provision(AppName appName, string env, string userName, string pcn, string query)
        {
            var result = string.Empty;
            if (appName == AppName.MOET) env = env + "_MOET";

            if (ConfigurationManager.ConnectionStrings[env] == null) throw new Exception("Invalid environment.");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[env].ConnectionString))
            {
                connection.Open();

                try
                {
                    var cmd = connection.CreateCommand();
                    if (appName == AppName.MSL)
                    {
                        if (!string.IsNullOrEmpty(pcn))
                        {
                            var cmd1 = connection.CreateCommand();

                            cmd1.CommandText =
                                "select OrganizationGUID from organization.dbo.organization (nolock) where publicnumber=@pcn";
                            cmd1.Parameters.AddWithValue("Pcn", pcn);

                            var data1 = cmd1.ExecuteScalar();
                            {
                                if (data1 == null) throw new Exception("PCN doesn't exist.");
                            }
                        }
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("User", userName);
                        cmd.Parameters.AddWithValue("Pcn", pcn);
                    }
                    else
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("User", userName);
                    }

                    if (appName == AppName.MSL)
                    {
                        using (var data = cmd.ExecuteXmlReader())
                        {
                            if (data.Read())
                            {
                                result = data.ReadOuterXml();
                            }
                        }
                    }
                    else
                    {
                        var data = cmd.ExecuteScalar();
                        {
                            if (data != null)
                            {
                                result = data.ToString();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number != 15063) throw;
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="query"></param>
        /// <param name="alias"></param>
        /// <param name="mq1"></param>
        /// <param name="mq2"></param>
        /// <param name="mq3"></param>
        /// <param name="mq4"></param>
        /// <param name="qsu"></param>
        /// <param name="mqf"></param>
        /// <param name="qv"></param>
        /// <returns></returns>
        public static bool ProvisionMsQuote(string env, string query, string alias, string mq1 = "D", string mq2 = "D", string mq3 = "D", string mq4 = "D", string qsu = "D", string mqf = "D", string qv = "D")
        {
            bool result;
            if (ConfigurationManager.ConnectionStrings[env] == null) throw new Exception("Invalid environment.");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[env].ConnectionString))
            {
                connection.Open();

                var email = ActiveDirectoryHelper.GetValue(alias, "mail");
                var fname = ActiveDirectoryHelper.GetValue(alias, "givenName");
                var lname = ActiveDirectoryHelper.GetValue(alias, "sn");

                try
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("alias", alias);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("fname", fname);
                    cmd.Parameters.AddWithValue("lname", lname);

                    cmd.Parameters.AddWithValue("MQ1", mq1);
                    cmd.Parameters.AddWithValue("MQ2", mq2);
                    cmd.Parameters.AddWithValue("MQ3", mq3);
                    cmd.Parameters.AddWithValue("MQ4", mq4);
                    cmd.Parameters.AddWithValue("QSU", qsu);
                    cmd.Parameters.AddWithValue("MQF", mqf);
                    cmd.Parameters.AddWithValue("QV", qv);

                    var adapter = new SqlDataAdapter(cmd);
                    var ds = new DataSet();

                    adapter.Fill(ds);

                    if (ds.Tables[1].Rows.Count == 0) throw new Exception("User is not provisioned.");

                    result = (mq1 == "I" && (ds.Tables[1].Rows.OfType<DataRow>().Where(item => item["ClaimType"].ToString() == "OperationalRole" && item["ClaimValue"].ToString() == "MQ1").FirstOrDefault() != null));
                    result |= (mq2 == "I" && (ds.Tables[1].Rows.OfType<DataRow>().Where(item => item["ClaimType"].ToString() == "OperationalRole" && item["ClaimValue"].ToString() == "MQ2").FirstOrDefault() != null));
                    result |= (mq3 == "I" && (ds.Tables[1].Rows.OfType<DataRow>().Where(item => item["ClaimType"].ToString() == "OperationalRole" && item["ClaimValue"].ToString() == "MQ3").FirstOrDefault() != null));
                    result |= (mq4 == "I" && (ds.Tables[1].Rows.OfType<DataRow>().Where(item => item["ClaimType"].ToString() == "OperationalRole" && item["ClaimValue"].ToString() == "MQ4").FirstOrDefault() != null));

                    result |= (qsu == "I" && (ds.Tables[1].Rows.OfType<DataRow>().Where(item => item["ClaimType"].ToString() == "OperationalRole" && item["ClaimValue"].ToString() == "QSU").FirstOrDefault() != null));
                    result |= (mqf == "I" && (ds.Tables[1].Rows.OfType<DataRow>().Where(item => item["ClaimType"].ToString() == "OperationalRole" && item["ClaimValue"].ToString() == "MQF").FirstOrDefault() != null));
                    result |= (qv == "I" && (ds.Tables[1].Rows.OfType<DataRow>().Where(item => item["ClaimType"].ToString() == "OperationalRole" && item["ClaimValue"].ToString() == "QV").FirstOrDefault() != null));

                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }

        public static bool ProvisionMopet(string env, string query, string alias)
        {
            bool result;
            env = env + "_MOPET";

            if (ConfigurationManager.ConnectionStrings[env] == null) throw new Exception("Invalid environment.");

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[env].ConnectionString))
            {
                connection.Open();

                var fname = ActiveDirectoryHelper.GetValue(alias, "givenName");
                var lname = ActiveDirectoryHelper.GetValue(alias, "sn");

                try
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("User", alias.Split('\\').Last());
                    cmd.Parameters.AddWithValue("Name", (fname + " " + lname).Trim());

                    var adapter = new SqlDataAdapter(cmd);
                    var ds = new DataSet();

                    adapter.Fill(ds);

                    if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0) throw new Exception("User is not provisioned.");

                    result = true;
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }
    }
}