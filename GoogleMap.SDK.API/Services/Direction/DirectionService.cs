using GoogleMap.SDK.API.Commons;
using GoogleMap.SDK.API.Commons.Models;
using GoogleMap.SDK.API.Enums;
using GoogleMap.SDK.API.Models.Requests;
using GoogleMap.SDK.API.Models.Responses;
using GoogleMap.SDK.API.Services.Direction.Request;
using GoogleMap.SDK.API.Services.Direction.Response;
using GoogleMap.SDK.API.Services.Place.PlaceDetail.Request;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var response = await PostAsync<DirectionNewResponse>(url, direction);
            return response;
        }

        public async Task<DirectionNewResponse> GetDirectionAsync(string origin, string destination, TrafficMode mode, List<Avoid> avoids, List<string> wayPoints)
        {
            DirectionNewRequest direction = new DirectionNewRequest(origin, destination, mode, avoids, wayPoints);
            var response = await PostAsync<DirectionNewResponse>(url, direction);
            return response;
        }
    }
}
