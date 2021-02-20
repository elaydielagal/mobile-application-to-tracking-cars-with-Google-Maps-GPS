using App3.clientApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace App3.view
{
    [DesignTimeVisible(false)]
    public partial class Maps : ContentPage
    {
        MapPageViewModel mapPageViewModel;
        public Maps()
        {
            InitializeComponent();
            BindingContext = mapPageViewModel = new MapPageViewModel();
            ApplyMapTheme();
        }
        private void ApplyMapTheme()
        {
           // this.GetType().Assembly.GetManifestResourceNames();
            var assembly = typeof(view.Maps).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"App3.MapResources.MapTheme.json");
            string themeFile;
            using (var reader = new System.IO.StreamReader(stream))
            {
                
                themeFile = reader.ReadToEnd();
                map.MapStyle = MapStyle.FromJson(themeFile);
            }
        }
        
        private bool TimerStarted()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);
                Compass.ReadingChanged += Compass_ReadingChanged;
                map.Pins.Clear();
                map.Polylines.Clear();
                //Get the cars nearrby from api but here we are hardcoding the contents
                var contents = await mapPageViewModel.LoadVehicles();
                if (contents != null)
                {
                    foreach (var item in contents)
                    {
                        Pin VehiclePins = new Pin()
                        {
                            Label = "Cars",
                            Type = PinType.Place,
                            Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                            Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),
                            Rotation = ToRotationPoints(headernothvalue)
                        };
                        map.Pins.Add(VehiclePins);
                    }
                }
            }

            );
            Compass.Stop();
            return true;
        }

        private float ToRotationPoints(double headernothvalue)
        {
            return (float)headernothvalue;

        }

        double headernothvalue;
        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            headernothvalue = data.HeadingMagneticNorth;
        }
        private void map_PinDragStart(object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {

        }

        public async void map_PinDragEnd(object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {
            var positions = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);//Latitude, Longitude
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));
            await DisplayAlert("Alert", "Pick up location : Latitude :" + e.Pin.Position.Latitude + " Longitude :" + e.Pin.Position.Longitude, "Ok");

        }

        private async void TrackPath_Clicked(object sender, EventArgs e)
        {
            /* ////this code not work for that we test with a file json
            HttpClient c;
            c = new HttpClient();
          
            int id0;
            var id = await SecureStorage.GetAsync("id_Mis");

            id0 = Convert.ToInt32(id);
            String trackPathFile;
            HttpResponseMessage r = await c.GetAsync("http://192.168.0.134/webApi/api/values/Geolocalisation/id=" + id0);
           
            trackPathFile = r.Content.ReadAsStringAsync().Result; */
            var assembly = typeof(view.Maps).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"App3.MapResources.TrackPath.json");
            string trackPathFile;

            using (var reader = new System.IO.StreamReader(stream))
            {
                trackPathFile = reader.ReadToEnd();
            } 
            var myJson = JsonConvert.DeserializeObject<List<Xamarin.Forms.GoogleMaps.Position>>(trackPathFile);


            map.Polylines.Clear();

            var polyline = new Xamarin.Forms.GoogleMaps.Polyline();
            polyline.StrokeColor = Color.Black;
            polyline.StrokeWidth = 3;

            foreach (var p in myJson)
            {
                polyline.Positions.Add(p);

            }
            map.Polylines.Add(polyline);

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Xamarin.Forms.GoogleMaps.Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));

          

          
          
        }
        //this function just when I test Gps from an tutorial
        /*  private void PickupButton_Clicked(object sender, EventArgs e)
          {
              //User Actual Location
              Pin VehiclePins = new Pin()
              {
                  Label = "Xamarin Guy",
                  Type = PinType.Place,
                  Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("PickupPin.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "PickupPin.png", WidthRequest = 30, HeightRequest = 30 }),
                  Position = new Position(Convert.ToDouble("28.126751"), Convert.ToDouble("82.297092")),
                  IsDraggable = true

              };
              map.Pins.Add(VehiclePins);
              //This is my actual location as of now we are taking it from google maps. But you have to use location plugin to generate latitude and longitude.
              var positions = new Position(28.126751, 82.297092);//Latitude, Longitude
              map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));
          }*/
        /*      async void Button_Clicked(System.Object sender, System.EventArgs e)
                {
                    var contents = await mapPageViewModel.LoadVehicles();

                    if (contents != null)
                    {
                        foreach (var item in contents)
                        {
                            Pin VehiclePins = new Pin()
                            {
                                Label = "Cars",
                                Type = PinType.Place,
                                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                                Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),


                            };
                            map.Pins.Add(VehiclePins);
                        }
                    }

                    //This is your location and it should be near to your car location.
                    var positions = new Position(28.126825, 82.297106);//Latitude, Longitude
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));

                }

                void Button_Clicked_1(System.Object sender, System.EventArgs e)
                {
                    var positions = new Position(28.126825, 82.297106);//Latitude, Longitude
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));

                    Device.StartTimer(TimeSpan.FromSeconds(5), () => TimerStarted());


                }*/

    }
}