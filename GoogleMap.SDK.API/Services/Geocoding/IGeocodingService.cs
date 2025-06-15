using GoogleMap.SDK.API.Services.Geocoding.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Geocoding
{
    public interface IGeocodingService
    {
        Task<GeocodingResponse> GetGeoCodingAsync(string address);
    }
}
