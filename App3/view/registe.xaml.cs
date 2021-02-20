using App3.clientApi;
using App3.service;
using Newtonsoft.Json.Linq;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class registe : ContentPage
    {
        public registe()
        {
            InitializeComponent();
        }
        private async void Buttonsave_Clicked(object sender, EventArgs e)
        {
            LoginService service = new LoginService();
            Register<Driver> reg = new Register<Driver>();
            var getLoginDetails = await service.CheckLoginIfExists(Username.Text, Password.Text);
            if (Fname.Text == null && Lname.Text == null && Username.Text == null && Phone.Text == null && Password.Text == null && CPassword.Text == null)
            {
                await DisplayAlert("Registration Failed", "Enter your details", "Okay", "Cancel");
            }
            else if (Password.Text != CPassword.Text)
            {
                await DisplayAlert("Registration Failed", " password error", "Okay", "Cancel");
            }
            else if (getLoginDetails)
            {
                await DisplayAlert("Registration Failed", " Already have an account", "Okay", "Cancel");
            }
            else
            {
                JObject o = await reg.SaveDriver(Fname.Text, Lname.Text, Username.Text, Password.Text, Phone.Text, Adress.Text);
                await DisplayAlert("Registration successeful", "added", "ok");
                await Navigation.PushAsync(new MainPage());

            }
        }
    }
}