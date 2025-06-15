using GoogleMap.SDK.API.Commons.Models;
using GoogleMap.SDK.API.Enums;
using GoogleMap.SDK.API.Models.Responses;
using GoogleMap.SDK.API.Services.Direction.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Direction
{
    public interface IDirectionService
    {
        Task<DirectionNewResponse> GetDirectionAsync(Location origin, Location destination, TrafficMode mode, List<Avoid> avoids, List<Location> wayPoints = null);
        Task<DirectionNewResponse> GetDirectionAsync(string origin, string destination, TrafficMode mode, List<Avoid> avoids, List<string> wayPoints = null);
    }
}
