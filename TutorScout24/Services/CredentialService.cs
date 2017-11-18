using System;
using System.Linq;
using Xamarin.Auth;

namespace TutorScout24.Services
{
    public class CredentialService:ICredentialService
    {
        public CredentialService(){
            
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
     
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }       

        /** From Xamarin.com **/
        public void SaveCredentials(string userName, string password)
        {
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                Account account = new Account
                {
                    Username = userName
                };
                account.Properties.Add("Password", password);
                AccountStore.Create().Save(account, "TutorScout24");
            }
        }

        public void DeleteCredentials()
        {
            var account = AccountStore.Create().FindAccountsForService("TutorScout24").FirstOrDefault();
            if (account != null)
            {
                AccountStore.Create().Delete(account, "TutorScout24");
            }
        }

        public bool DoCredentialsExist()
        {
            return AccountStore.Create().FindAccountsForService("TutorScout24").Any() ? true : false;
        }
        /** From Xamarin.com **/
    }
}
