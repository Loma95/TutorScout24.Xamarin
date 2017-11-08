using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TutorScout24.Models;

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
    }
}
