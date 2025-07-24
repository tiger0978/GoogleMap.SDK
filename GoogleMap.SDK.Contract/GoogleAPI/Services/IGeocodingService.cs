using GoogleMap.SDK.Contracts.GoogleAPI.Models.Geocoding.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.Services
{
    public interface IGeocodingService
    {
        Task<GeocodingResponse> GetGeoCodingAsync(string address);
    }
}
