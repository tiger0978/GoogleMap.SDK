using GoogleMap.SDK.API.Commons.Models;
using GoogleMap.SDK.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Direction.Request
{
    public class DirectionNewRequest
    {
        public DirectionNewRequest(Location origin, Location destination, TrafficMode trafficMode, List<Avoid> avoids, List<Location> wayPoints = null)
        {
            this.origin.location = origin;
            this.destination.location = destination;
            this.travelMode = trafficMode.ToString();
            foreach (var avoid in avoids)
            {
                switch (avoid) 
                {
                    case Avoid.ferries:
                        this.routeModifiers.avoidFerries = true;
                        break;
                    case Avoid.highways:
                        this.routeModifiers.avoidHighways = true;
                        break;
                    case Avoid.tolls:
                        this.routeModifiers.avoidTolls = true;
                        break;
                }
            }
            if (wayPoints != null)
            {
                this.intermediates = wayPoints.Select(x => new Intermediate() { location = x }).ToArray();
            }
        }

        public DirectionNewRequest(string originId, string destinationId, TrafficMode trafficMode, List<Avoid> avoids, List<string> wayPoints = null)
        {
            this.origin.placeId = originId;
            this.destination.placeId = destinationId;
            this.travelMode = trafficMode.ToString();
            foreach (var avoid in avoids)
            {
                switch (avoid)
                {
                    case Avoid.ferries:
                        this.routeModifiers.avoidFerries = true;
                        break;
                    case Avoid.highways:
                        this.routeModifiers.avoidHighways = true;
                        break;
                    case Avoid.tolls:
                        this.routeModifiers.avoidTolls = true;
                        break;
                }
            }
            if (wayPoints != null) 
            {
                this.intermediates = wayPoints.Select(x => new Intermediate() { placeId = x }).ToArray();
            }
        }


        public Origin origin { get; set; } = new Origin();
        public Destination destination { get; set; }  = new Destination();
        public Intermediate[] intermediates { get; set; }
        public string travelMode { get; set; }
        public string routingPreference { get; set; }
        public bool computeAlternativeRoutes { get; set; }
        public Routemodifiers routeModifiers { get; set; } = new Routemodifiers();
        public string languageCode { get; set; }
        public string units { get; set; }

        public class Origin
        {
            public Location location { get; set; }
            public bool sideOfRoad { get; set; }
            public string placeId { get; set; }
        }

        public class Destination
        {
            public Location location { get; set; }
            public string placeId { get; set; }

        }

        public class Routemodifiers
        {
            public bool avoidTolls { get; set; }
            public bool avoidHighways { get; set; }
            public bool avoidFerries { get; set; }
        }

        public class Intermediate
        {
            public Location location { get; set; }
            public string placeId { get; set; }
        }
    }
}
