using GoogleMap.SDK.API.Services.Direction;
using GoogleMap.SDK.API.Services.Geocoding;
using GoogleMap.SDK.API.Services.Place;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API
{
    public class GoogleAPIContext:IGoogleAPIContext
    {
        private static string _apiKey;
        public static String APIKey => _apiKey;
        public IDirectionService Direction { get; set; }
        public IGeocodingService Geocoding { get; set; }
        public IPlaceService Place { get; set; }

        public GoogleAPIContext(IDirectionService directionService, IGeocodingService geocodingService, IPlaceService placeService)
        {
            Direction = directionService;
            Geocoding = geocodingService;
            Place = placeService;
        }
    }
}
