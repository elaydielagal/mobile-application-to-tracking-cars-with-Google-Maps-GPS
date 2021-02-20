using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class itemDetails : ContentPage
    {
        public itemDetails()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient c;
            c = new HttpClient();
            List<Report> list;
            int id0;
            var id = await SecureStorage.GetAsync("id_Mis");
           
            id0 = (int)Convert.ToInt32(id);          
            String s;
            HttpResponseMessage r = await c.GetAsync("http://192.168.0.134/webApi/api/values/getUserMissions/report/id="+id);
            
            s = r.Content.ReadAsStringAsync().Result;
            list = JsonConvert.DeserializeObject<List<Report>>(s);
            //Console.WriteLine(list);
            MyListView.ItemsSource = list;
 

        }
        public async void GetLocation()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(5)
                });
            }
            loc.Text = "Lat :" + location.Latitude.ToString() + "long : " + location.Longitude.ToString();
        }

        private void BtnLocation_Clicked(object sender, EventArgs e)
        {
            GetLocation();
        }

        public async void AddReport_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReport());
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Maps());
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx.Message);
            }
        }
    }
}