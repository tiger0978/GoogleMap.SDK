using GoogleMap.SDK.API.Commons;
using GoogleMap.SDK.API.Services.Geocoding.Request;
using GoogleMap.SDK.API.Services.Geocoding.Response;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Geocoding
{
    public class GeocodingService : BaseService, IGeocodingService
    {
        public GeocodingService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<GeocodingResponse> GetGeoCodingAsync(string address)
        {
            BaseRequest geocodingRequest = new GeocodingRequest(address);
            var response = await GetAsync<GeocodingResponse>(geocodingRequest.URL);
            return response;
        }
    }
}
