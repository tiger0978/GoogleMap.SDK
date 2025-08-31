using GoogleMap.SDK.API.Commons;
using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.Direction.Request;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.Direction.Response;
using GoogleMap.SDK.Contracts.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMap.SDK.Contract.Utility.Converter;

namespace GoogleMap.SDK.API.Services.Direction
{
    public class DirectionService : BaseService, IDirectionService
    {
        public string url = "https://routes.googleapis.com/directions/v2:computeRoutes";

        public DirectionService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<DirectionNewResponse> GetDirectionAsync(Location origin, Location destination, TrafficMode mode, List<Avoid> avoids, List<Location> wayPoints)
        {
            DirectionNewRequest direction = new DirectionNewRequest(origin, destination, mode, avoids, wayPoints);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = { new PolylineConverter() }
            };

            var response = await PostAsync<DirectionNewResponse>(url, direction, settings);
            return response;
        }

        public async Task<DirectionNewResponse> GetDirectionAsync(string origin, string destination, TrafficMode mode, List<Avoid> avoids, List<string> wayPoints)
        {
            DirectionNewRequest direction = new DirectionNewRequest(origin, destination, mode, avoids, wayPoints);

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = { new PolylineConverter() }
            };

            var response = await PostAsync<DirectionNewResponse>(url, direction, settings);
            return response;
        }
    }
}
