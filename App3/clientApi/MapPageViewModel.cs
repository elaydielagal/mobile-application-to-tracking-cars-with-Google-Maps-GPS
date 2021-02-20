using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App3.clientApi
{
    public class MapPageViewModel
    {
        //this class just a test
        public MapPageViewModel()
        {
        }


        public class VehicleLocations
        {
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }

        internal async Task<List<VehicleLocations>> LoadVehicles()
        {
            //Call the api to get the vehicles nearby

            //This are the hardcoded data
            List<VehicleLocations> vehicleLocations = new List<VehicleLocations>
            {
                new VehicleLocations{Latitude = 28.128888,Longitude=82.296891},
                new VehicleLocations{Latitude = 28.130061,Longitude=82.297364},
                new VehicleLocations{Latitude = 28.129550,Longitude=82.298887},
                new VehicleLocations{Latitude = 28.127336,Longitude=82.292106},

            };
            return vehicleLocations;
        }
    }

}

