using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace App3
{

    public class ClientApi<T>
        {
            private const string hostUrl = "http://192.168.0.134/webApi/";  
            private const string uri = hostUrl + "api/values/UserCredentials/";  

             
            public async Task<bool> checkLogin(string username, string password)
            {
            //Console.WriteLine(username, password);
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(uri + "username=" + username + "/" + "password=" + password);
           
                return response.IsSuccessStatusCode; // return   true or false
            }
        public async Task<Driver> checkLogin1(string username, string password)
        {
            //Console.WriteLine(username, password);
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(uri + "username=" + username + "/" + "password=" + password);

            if (response.IsSuccessStatusCode)
            {
                string result=response.Content.ReadAsStringAsync().Result;
                try
                {
                    return JsonConvert.DeserializeObject<Driver>(result);
                }
                catch (Exception e)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
