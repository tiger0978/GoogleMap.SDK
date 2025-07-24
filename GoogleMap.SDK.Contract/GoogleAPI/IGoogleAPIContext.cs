using GoogleMap.SDK.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.GoogleAPI
{
    public interface IGoogleAPIContext
    {
        IDirectionService Direction { get; set; }
        IGeocodingService Geocoding { get; set; }
        IPlaceService Place { get; set; }

    }
}
