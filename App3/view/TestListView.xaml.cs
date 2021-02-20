using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class TestListView : ContentPage
    {
        string ipAdd = "http://192.168.0.134/webApi";
        public ObservableCollection<string> Items { get; set; }
        public TestListView()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            HttpClient c;
            c = new HttpClient();
            List<Mission> list;
            int id0;
            var id = await SecureStorage.GetAsync("id_driver");
           if(id != null) {
                  id0 = Convert.ToInt32(id);
            }
            else { id0 = 1; }
            
           
            String s;
            HttpResponseMessage r = await c.GetAsync(ipAdd + "/api/values/getUserMissions/id="+id0);
            //s = await c.GetStringAsync("http://192.168.0.116/webApi/api/values/getDrivers");

            s = r.Content.ReadAsStringAsync().Result;
            list = JsonConvert.DeserializeObject<List<Mission>>(s);
            //Console.WriteLine(list);
            MyListView.ItemsSource = list;
           







        }

        public async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            Label btn = (Label)sender;
            Object textValue = btn.Text;
            var id = await SecureStorage.GetAsync("id_Mis");
            if (id == null) {
                await SecureStorage.SetAsync("id_Mis", textValue.ToString());
            }
            else {
                   SecureStorage.Remove("id_Mis");

                await SecureStorage.SetAsync("id_Mis", textValue.ToString());

            }
            await Navigation.PushAsync(new itemDetails());
        }

        

    
    }
}