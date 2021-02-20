using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App3.clientApi
{
	public class Register<T>
	{
		public Register() { }

		public async Task<JObject> SaveDriver( string Fname, string Lname, string Username, string Password, string Phone, string Adress )
		{try
			{
				int phone = Convert.ToInt32(Phone.Trim());
				Driver driver = new Driver();
				driver.Fname = Fname.Trim();
				driver.Lname = Lname.Trim();
				driver.username = Username.Trim();
				driver.Phone = phone;
				driver.password = Password.Trim();
				driver.Adress = Adress.Trim();
				Uri uri;
				string json;
				StringContent content;
				HttpResponseMessage response = null;
				var httpClient = new HttpClient();

				uri = new Uri(string.Format("http://192.168.0.134/webApi/api/values/newDriver", string.Empty));
				json = JsonConvert.SerializeObject(driver);
				content = new StringContent(json, Encoding.UTF8, "application/json");
				response = await httpClient.PostAsync(uri, content);
				 
				if (response.IsSuccessStatusCode)
				{
					string res = response.Content.ReadAsStringAsync().Result;
					JObject jRes = JObject.Parse(res);
					return jRes;
				 
				}
                else {
					string res = "error ";
					JObject jres = JObject.Parse(res);
					return jres;
				}
				 
			}

			catch (Exception ex)
			{
				string res = "error ";
				JObject jres = JObject.Parse(res);
				return jres;
			}
		}
	
	
	}
}
