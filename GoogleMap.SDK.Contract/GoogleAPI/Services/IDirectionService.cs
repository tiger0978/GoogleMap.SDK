using GoogleMap.SDK.Contracts.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Contracts.GoogleAPI.Models.Direction.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.Services
{
    public interface IDirectionService
    {
        Task<DirectionNewResponse> GetDirectionAsync(Location origin, Location destination, TrafficMode mode, List<Avoid> avoids, List<Location> wayPoints = null);
        Task<DirectionNewResponse> GetDirectionAsync(string origin, string destination, TrafficMode mode, List<Avoid> avoids, List<string> wayPoints = null);
    }
}
