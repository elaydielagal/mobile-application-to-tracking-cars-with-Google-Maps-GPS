using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace App3.clientApi
{
    public class AddReport<T>
	{
		public AddReport() { }
		public async Task<JObject> SaveReport(string name_rapport, string commentary_problem)
		{
			try
			{
				var id = await SecureStorage.GetAsync("id_Mis");

				int id_m = Convert.ToInt32(id);
				Report report = new Report();
				report.id_mission = id_m;
				report.name_rapport = name_rapport.Trim();
				report.commentary_problem = commentary_problem.Trim();

				Uri uri;
				string json;
				StringContent content;
				HttpResponseMessage response = null;
				var httpClient = new HttpClient();

				uri = new Uri(string.Format("http://192.168.0.134/webApi/api/values/newReport", string.Empty));
				json = JsonConvert.SerializeObject(report);
				content = new StringContent(json, Encoding.UTF8, "application/json");
				response = await httpClient.PostAsync(uri, content);

				if (response.IsSuccessStatusCode)
				{
					string res = response.Content.ReadAsStringAsync().Result;
					JObject jRes = JObject.Parse(res);
					return jRes;

				}
				else
				{
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
