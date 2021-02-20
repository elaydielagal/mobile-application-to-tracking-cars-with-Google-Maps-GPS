using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App3.clientApi;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddReport : ContentPage
    {
        public AddReport()
        {
            InitializeComponent();
        }

        public async void Buttonsave_Clicked(object sender, EventArgs e)
        {
            AddReport<Report> reg = new AddReport<Report>();
            if (name_rapport.Text == null && commentary_problem.Text == null)
            {
                await DisplayAlert("Add Report Failed", "Enter your details", "Okay");
            }
          
            else
            {
                JObject o = await reg.SaveReport(name_rapport.Text, commentary_problem.Text);
                await DisplayAlert("Registration successeful", "added", "ok");
                await Navigation.PushAsync(new TestListView());

            }
        }
    }
}