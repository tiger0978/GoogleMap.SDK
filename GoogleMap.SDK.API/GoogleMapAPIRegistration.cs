using GoogleMap.SDK.API.Services.Direction;
using GoogleMap.SDK.API.Services.Geocoding;
using GoogleMap.SDK.API.Services.Place;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API
{
    public static class GoogleMapAPIRegistration
    {

        public static void AddGoogleMapAPIRegistration(this IServiceCollection services)
        {
            services.AddTransient<IGeocodingService, GeocodingService>();
            services.AddTransient<IDirectionService, DirectionService>();
            services.AddTransient<IPlaceService, PlaceService>();
            services.AddTransient<IGoogleAPIContext, GoogleAPIContext>();
        }
    }
}
