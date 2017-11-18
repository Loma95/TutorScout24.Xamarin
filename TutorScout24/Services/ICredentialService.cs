using System;
namespace TutorScout24.Services
{
    public interface ICredentialService
    {
        string UserName { get; }

        string Password { get; }

        void SaveCredentials(string userName, string password);

        void DeleteCredentials();

        bool DoCredentialsExist();
    }
}
