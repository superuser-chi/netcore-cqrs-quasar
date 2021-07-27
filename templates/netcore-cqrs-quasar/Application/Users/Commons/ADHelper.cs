using System;
using System.DirectoryServices.AccountManagement;
using Domain.Configuration;

namespace Application.Users.Commons
{
    public class ADHelper
    {
        public static bool ValidateAgainstAD(AppConfig appConfig, String username, String password)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, appConfig.Domain))
                {
                    var adUsername = ADHelper.GetAdUserName(appConfig, username);
                    if (context.ValidateCredentials(adUsername, password, ContextOptions.Negotiate))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }
        }
        public static String GetAdUserName(AppConfig appConfig, String userName)
        {
            try
            {
                using (var context = new PrincipalContext(ContextType.Domain, appConfig.Domain))
                {
                    var adUser = UserPrincipal.FindByIdentity(context, userName);
                    if (adUser == null)
                    {
                        throw new InvalidOperationException();
                    }
                    return adUser.SamAccountName;
                }
            }
            catch (Exception)
            {
                return userName;
            }
        }

    }
}
