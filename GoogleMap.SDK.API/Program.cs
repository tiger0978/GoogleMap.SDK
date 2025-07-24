using GoogleMap.SDK.API.Services.Direction;
using GoogleMap.SDK.API.Services.Geocoding;
using GoogleMap.SDK.API.Services.Place;
using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI;
using IoC_Container;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(IConfiguration),configuration));
            services.AddGoogleMapAPIRegistration();

            var provides = services.BuildServiceProvider();

            IGoogleAPIContext context = provides.GetService<IGoogleAPIContext>();
            var result = await context.Geocoding.GetGeoCodingAsync("台北101");

            Location ori = new Location() { latLng = new Latlng { latitude = 37.419734, longitude = -122.0827784 } };
            Location des = new Location() { latLng = new Latlng { latitude = 37.417670, longitude = -122.079595 } };

            var route = await context.Direction.GetDirectionAsync(ori, des, TrafficMode.DRIVE, new List<Avoid>() { Avoid.highways });


            //IGeocodingService service = new GeocodingService();
            //var result = await service.GetGeoCodingAsync("台北101");

            //IDirectionService service = new DirectionService("AIzaSyDDuITwu1FCDxtEohz-1KFSXz0cJqUeeh0");
            //Latlng ori = new Latlng()
            //{
            //    latitude = (float)37.419734,
            //    longitude = (float)-122.0827784
            //};
            //Latlng dest = new Latlng()
            //{
            //    latitude = (float)37.417670,
            //    longitude = (float)-122.079595
            //};
            //Location origin = new Location()
            //{
            //    latLng = ori
            //};
            //Location destination = new Location()
            //{
            //    latLng = dest
            //};
            //Latlng point = new Latlng()
            //{
            //    latitude = (float)37.419734,
            //    longitude = (float)-122.0807784
            //};
            //Location wayPoint = new Location()
            //{
            //    latLng = point
            //};
            //List<Location> wayPoints = new List<Location>();
            //wayPoints.Add(wayPoint);
            //List<Avoid> avoids = new List<Avoid>();
            //avoids.Add(Avoid.highways);
            //avoids.Add(Avoid.ferries);
            //var response = await service.Direction("ChIJayOTViHY5okRRoq2kGnGg8o", "ChIJTYKK2G3X5okRgP7BZvPQ2FU", TrafficMode.DRIVE, avoids, null);
            //var response = await service.Direction(origin, destination, TrafficMode.DRIVE, avoids, wayPoints);

            Console.ReadKey();

        }
    }
}
