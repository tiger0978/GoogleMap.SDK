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
    public interface IGoogleAPIContext
    {
        IDirectionService Direction { get; set; }
        IGeocodingService Geocoding { get; set; }
        IPlaceService Place { get; set; }

    }
}
