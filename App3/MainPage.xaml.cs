using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using App3.view;
using App3.service;
namespace App3
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void ButtonLogin_Clicked(object sender, EventArgs e)
        {

            LoginService services = new LoginService();
            //var getLoginDetails = await services.CheckLoginIfExists(EntryUsername.Text, EntryPassword.Text);
            var getDriverDetail = await services.CheckLoginIfExist(EntryUsername.Text, EntryPassword.Text);

            if (getDriverDetail != null)
            {
                var id = await SecureStorage.GetAsync("id_driver");
                if (id == null)
                {
                    await SecureStorage.SetAsync("id_driver", getDriverDetail.id_driver.ToString());
                }
                else
                {
                    SecureStorage.Remove("id_driver");

                    await SecureStorage.SetAsync("id_driver", getDriverDetail.id_driver.ToString());

                }
               


                await Navigation.PushAsync(new TestListView());

            }
            else
            {
                await DisplayAlert("Login failed", "Username or Password is incorrect or not exists", "Okay");
            }
        }

        private async void ButtonSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new registe());

        }
    }
}
 
