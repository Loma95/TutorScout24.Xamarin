﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TutorScout24.Models;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using MvvmNano;
using TutorScout24.ViewModels;
using TutorScout24.Models.Chat;
using System.Collections.ObjectModel;

namespace TutorScout24.Services
{
    public class TutorScoutRestService
    {

        String RestUrl;
        HttpClient client;
      
        public TutorScoutRestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<string> CreateUser(User usr)
        {
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/create";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(usr);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine(json);
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return "true";

            }
            return await response.Content.ReadAsStringAsync();
 
        }

        public async Task<bool> SendMessage(SendMessage msg)
        {
            msg.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/message/sendMessage";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(msg);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;

        }

        public async Task<List<RestMessage>> GetReceivedMessages()
        {
            RestCommandWithAuthentication cmd = new RestCommandWithAuthentication();
            cmd.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/message/getReceivedMessages";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(cmd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var msgs = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(msgs);
                return JsonConvert.DeserializeObject<List<RestMessage>>(msgs);

            }
            return null;

        }

        public async Task<List<RestMessage>> GetSentMessages()
        {
            RestCommandWithAuthentication cmd = new RestCommandWithAuthentication();
            cmd.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/message/getSentMessages";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(cmd);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var msgs = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RestMessage>>(msgs);

            }
            return null;

        }


        public async Task<bool> UpdateUser(RestCommandWithAuthentication cmd)
        {
            cmd.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/updateUser";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(cmd,Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine(json);
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;

        }


        public async Task<UserInfos> GetUserInfos()
        {
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/info";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var response = await client.GetAsync(uri);
            Debug.WriteLine(await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(content);
                return JsonConvert.DeserializeObject<UserInfos>(content);
            }else{
                return null;
            }
        }

        public async Task<bool> CanAuthenticate(CheckAuthentication auth)
        {
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/checkAuthentication";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(auth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            Debug.WriteLine(response.ReasonPhrase);
            Debug.WriteLine(response.StatusCode);
            return false;
        }

        public async Task<MyUserInfo> GetMyUserInfo()
        {

            FindUser _findUser = new FindUser();
            _findUser.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            _findUser.userToFind = MvvmNano.MvvmNanoIoC.Resolve<Authentication>().userName;


            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/myUserInfo";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(_findUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<MyUserInfo>(rescontent);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserInfo> GetUserInfo(string username)
        {

            FindUser _findUser = new FindUser();
            _findUser.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            _findUser.userToFind = username;


            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/userInfo";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(_findUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserInfo>(rescontent);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Tutoring>> GetTutorings()
        {
            Debug.WriteLine("Getting Offers");
            LocationService serv = LocationService.getInstance();
            var tutoringRequest = new TutoringRequest
            {
                latitude = (int)(await serv.GetPosition()).Latitude,
                longitude = (int)(await serv.GetPosition()).Longitude,
                authentication = MvvmNanoIoC.Resolve<Authentication>(),
                rangeKm = 100000,
                rowLimit = 50,
                rowOffset = 0
            };
            Debug.WriteLine(MvvmNanoIoC.Resolve<Authentication>().userName);
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/tutoring/";
            if (MasterDetailViewModel.CurrentMode.Equals(MasterDetailViewModel.Mode.STUDENT))
            {
                RestUrl += "offers";
            } else
            {
                RestUrl += "requests";
            }

            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(tutoringRequest);
            Debug.WriteLine("JSON Serialized:" + json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine("Got Offers");
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Tutoring>>(rescontent);
            }
            else
            {
                Debug.WriteLine(await response.Content.ReadAsStringAsync());
                return null;
            }
        }

        public async Task<bool> CreateTutoring(CreateTutoring ct)
        {
            LocationService serv = LocationService.getInstance();
            ct.authentication = MvvmNanoIoC.Resolve<Authentication>();
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/tutoring/create";
            if (MasterDetailViewModel.CurrentMode.Equals(MasterDetailViewModel.Mode.STUDENT))
            {
                RestUrl += "Request";
            }
            else
            {
                RestUrl += "Offer";
            }

            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(ct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            Debug.WriteLine("tutoring" + response.Content);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<MyTutoring>> GetMyTutorings()
        {
            RestCommandWithAuthentication auth = new RestCommandWithAuthentication()
            {
                authentication = MvvmNanoIoC.Resolve<Authentication>()
            };


            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/tutoring/my";

            if (MasterDetailViewModel.CurrentMode.Equals(MasterDetailViewModel.Mode.STUDENT))
            {
                RestUrl += "Requests";
            }
            else
            {
                RestUrl += "Offers";
            }

            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(auth);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine(json);
            var response = await client.PostAsync(uri, content);
            Debug.WriteLine("Response:" + await response.Content.ReadAsStringAsync());
            if (response.IsSuccessStatusCode)
            {
                var rescontent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<MyTutoring>>(rescontent);
            }
            else
            {
                return null;
            }
            
        }


     

    }
}
