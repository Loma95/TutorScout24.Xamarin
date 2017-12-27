using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MvvmNano;
using Newtonsoft.Json;
using TutorScout24.Models;
using TutorScout24.Models.Chat;
using TutorScout24.Models.Tutorings;
using TutorScout24.Models.UserData;
using TutorScout24.ViewModels;

namespace TutorScout24.Services
{
    /// <summary>
    ///     Class for communication with TutorScout backend
    /// </summary>
    public class TutorScoutRestService
    {
        private readonly HttpClient _client;

        private string _restUrl;

        public TutorScoutRestService()
        {
            _client = new HttpClient {MaxResponseContentBufferSize = 256000};
        }

        /// <summary>
        ///     Create a new User
        /// </summary>
        /// <param name="usr">User to create</param>
        /// <returns>true if successfull, error string otherwise</returns>
        public async Task<string> CreateUser(User usr)
        {
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/create";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(usr);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine(json);
            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
                return "true";
            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        ///     Send a Message
        /// </summary>
        /// <param name="msg">Message to send</param>
        /// <returns>true if successfull, false otherwise</returns>
        public async Task<bool> SendMessage(SendMessage msg)
        {
            msg.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/message/sendMessage";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(msg);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        ///     Deletes a message
        /// </summary>
        /// <param name="messageId">Number of message to delete</param>
        /// <returns>true if successfull, false otherwise</returns>
        public async Task<bool> DeleteMessage(int messageId)
        {
            var dm = new DeleteMessage();
            dm.messageId = messageId;
            dm.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/message/deleteMessage";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(dm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, content);
            Debug.WriteLine(response.Content.ReadAsStringAsync());
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        ///     Get recieved messages for currently logged in user
        /// </summary>
        /// <returns>List of RestMessages that the user has recieved.</returns>
        public async Task<List<RestMessage>> GetReceivedMessages()
        {
            var cmd = new RestCommandWithAuthentication();
            cmd.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/message/getReceivedMessages";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(cmd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var msgs = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(msgs);
                return JsonConvert.DeserializeObject<List<RestMessage>>(msgs);
            }
            return null;
        }

        /// <summary>
        ///     Get sent messages for currently logged in user
        /// </summary>
        /// <returns>List of RestMessages that the user has sent.</returns>
        public async Task<List<RestMessage>> GetSentMessages()
        {
            var cmd = new RestCommandWithAuthentication();
            cmd.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/message/getSentMessages";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(cmd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var msgs = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RestMessage>>(msgs);
            }
            return null;
        }

        /// <summary>
        ///     Update User
        /// </summary>
        /// <param name="cmd">Authentication</param>
        /// <returns>true if successfull, false otherwise</returns>
        public async Task<bool> UpdateUser(RestCommandWithAuthentication cmd)
        {
            cmd.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/updateUser";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(cmd, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine(json);
            HttpResponseMessage response = null;
            response = await _client.PutAsync(uri, content);
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        ///     Gets User Infos
        /// </summary>
        /// <returns>User Info</returns>
        public async Task<UserInfos> GetUserInfos()
        {
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/info";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var response = await _client.GetAsync(uri);
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                return JsonConvert.DeserializeObject<UserInfos>(content);
            }
            return null;
        }

        /// <summary>
        ///     Checks auth
        /// </summary>
        /// <param name="auth">Authentication RequestObject</param>
        /// <returns>success/failure</returns>
        public async Task<bool> CanAuthenticate(CheckAuthentication auth)
        {
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/checkAuthentication";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(auth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await _client.PostAsync(uri, content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        ///     Info for current User
        /// </summary>
        /// <returns>Info for currently logged in user</returns>
        public async Task<MyUserInfo> GetMyUserInfo()
        {
            var _findUser = new FindUser();
            _findUser.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _findUser.userToFind = MvvmNanoIoC.Resolve<Authentication>().userName;


            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/myUserInfo";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(_findUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MyUserInfo>(rescontent);
            }
            return null;
        }

        /// <summary>
        ///     Get User info for input string
        /// </summary>
        /// <param name="username">user to get info for</param>
        /// <returns>user info</returns>
        public async Task<UserInfo> GetUserInfo(string username)
        {
            var _findUser = new FindUser();
            _findUser.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _findUser.userToFind = username;


            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/userInfo";
            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(_findUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserInfo>(rescontent);
            }
            return null;
        }

        /// <summary>
        ///     Gets Tutorings in ragne of 100000km, maximum 50
        /// </summary>
        /// <returns>List of Tutorings</returns>
        public async Task<List<Tutoring>> GetTutorings()
        {
            Debug.WriteLine("Getting Offers");
            var serv = LocationService.GetInstance();
            var tutoringRequest = new TutoringRequest
            {
                latitude = (int) (await serv.GetPosition()).Latitude,
                longitude = (int) (await serv.GetPosition()).Longitude,
                authentication = MvvmNanoIoC.Resolve<Authentication>(),
                rangeKm = 100000,
                rowLimit = 50,
                rowOffset = 0
            };
            Debug.WriteLine(MvvmNanoIoC.Resolve<Authentication>().userName);
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/tutoring/";

            //Add corresponding string
            if (MasterDetailViewModel.CurrentMode.Equals(MasterDetailViewModel.Mode.STUDENT))
                _restUrl += "offers";
            else
                _restUrl += "requests";

            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(tutoringRequest);
            Debug.WriteLine("JSON Serialized:" + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Got Offers");
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Tutoring>>(rescontent);
            }
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
            return null;
        }

        /// <summary>
        ///     Create a new tutoring
        /// </summary>
        /// <param name="ct">tutoring to create</param>
        /// <returns>Success/failure</returns>
        public async Task<bool> CreateTutoring(CreateTutoring ct)
        {
            var serv = LocationService.GetInstance();
            ct.authentication = MvvmNanoIoC.Resolve<Authentication>();
            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/tutoring/create";
            if (MasterDetailViewModel.CurrentMode.Equals(MasterDetailViewModel.Mode.STUDENT))
                _restUrl += "Request";
            else
                _restUrl += "Offer";

            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(ct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(uri, content);
            Debug.WriteLine("tutoring" + response.Content);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        ///     Retrieves tutorings for current user
        /// </summary>
        /// <returns>List of user's tutorings</returns>
        public async Task<List<MyTutoring>> GetMyTutorings()
        {
            var auth = new RestCommandWithAuthentication
            {
                authentication = MvvmNanoIoC.Resolve<Authentication>()
            };

            _restUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/tutoring/my";

            if (MasterDetailViewModel.CurrentMode.Equals(MasterDetailViewModel.Mode.STUDENT))
                _restUrl += "Requests";
            else
                _restUrl += "Offers";

            var uri = new Uri(string.Format(_restUrl, string.Empty));
            var json = JsonConvert.SerializeObject(auth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine(json);
            var response = await _client.PostAsync(uri, content);
            Debug.WriteLine("Response:" + await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MyTutoring>>(rescontent);
            }
            return null;
        }
    }
}