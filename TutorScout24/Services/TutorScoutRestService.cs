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

        public async Task<bool> CreateUser(User usr)
        {
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/create";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(usr);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return true;

            }
            return false;
 
        }


        public async Task<bool> UpdateUser(UpdateUser usr)
        {
            usr.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/updateUser";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(usr);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Debug.WriteLine(json);
            HttpResponseMessage response = null;
            response = await client.PutAsync(uri, content);
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
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
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

        public async Task<UserInfo> GetMyUserInfo()
        {

            FindUser _findUser = new FindUser();
            _findUser.authentication = MvvmNano.MvvmNanoIoC.Resolve<Authentication>();
            _findUser.userToFind = MvvmNano.MvvmNanoIoC.Resolve<Authentication>().userName;


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
   

        public List<Tutoring> GetTutorings()
        {
            var assembly = typeof(TutorScout24.App).GetTypeInfo().Assembly;
                
             Stream stream = assembly.GetManifestResourceStream("TutorScout24.MockData.TutoringData.json");
           
             string text = "";

             using (var reader = new System.IO.StreamReader(stream))
             {
                 text = reader.ReadToEnd();
             }

            Debug.WriteLine(text);
           var list = JsonConvert.DeserializeObject<List<Tutoring>>(text);
        
            return list;
        }
    }
}
