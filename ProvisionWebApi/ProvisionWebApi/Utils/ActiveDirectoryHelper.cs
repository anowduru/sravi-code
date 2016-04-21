using System;
using System.DirectoryServices;
using System.Security.Principal;

namespace ProvisionWebApi.Utils
{
    public static class ActiveDirectoryHelper
    {
        public static string GetValue(string userName, string property)
        {
            using (DirectoryEntry entry = GetDirectoryObject(userName))
            {
                return (entry.Properties[property].Value ?? "").ToString();
            }
        }

        public static void ValidateUser(string userName)
        {
            GetDirectoryObject(userName);
        }

        private static DirectoryEntry GetDirectoryObject(string userName)
        {
            string[] nameValue = userName.Split('\\');

            string filter = string.Format("(&(ObjectClass={0})(sAMAccountName={1}))", "person", nameValue[1]);
            string domain = nameValue[0] + ".corp.microsoft.com";

            string[] properties = new string[] { "fullname" };

            using (DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domain, null, null, AuthenticationTypes.Secure))
            {
                DirectorySearcher searcher = new DirectorySearcher(adRoot);
                searcher.SearchScope = SearchScope.Subtree;
                searcher.ReferralChasing = ReferralChasingOption.All;
                searcher.PropertiesToLoad.AddRange(properties);
                searcher.Filter = filter;

                SearchResult result = searcher.FindOne();

                if (result == null) throw new Exception(string.Format("User {0} not found in active directory.", userName));

                DirectoryEntry directoryEntry = result.GetDirectoryEntry();

                return directoryEntry;
            }
        }
    }
}
