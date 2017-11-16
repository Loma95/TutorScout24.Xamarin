using System;
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

        public async Task<string> CreateUser(User usr)
        {
            RestUrl = "http://tutorscout24.vogel.codes:3000/tutorscout24/api/v1/user/create";
            var uri = new Uri(string.Format(RestUrl, string.Empty));
            var json = JsonConvert.SerializeObject(usr);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return "Success";

            }
            return "Failed";
 
        }


        public async Task<UserInfos> GetUserInfo()
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
