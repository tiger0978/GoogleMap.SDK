using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.GoogleAPI.Models.Direction.Response
{
    public class DirectionNewResponse
    {

        public Route[] routes { get; set; }
        public Geocodingresults geocodingResults { get; set; }

        public class Geocodingresults
        {
        }

        public class Route
        {
            public Leg[] legs { get; set; }
            public int distanceMeters { get; set; }
            public string duration { get; set; }
            public string staticDuration { get; set; }
            public Polyline polyline { get; set; }
            public Viewport viewport { get; set; }
            public Traveladvisory travelAdvisory { get; set; }
            public Localizedvalues localizedValues { get; set; }
            public string routeToken { get; set; }
            public string[] routeLabels { get; set; }
            public Polylinedetails polylineDetails { get; set; }
        }

        public class Polyline
        {
            public string encodedPolyline { get; set; }
        }

        public class Viewport
        {
            public Low low { get; set; }
            public High high { get; set; }
        }

        public class Low
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class High
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class Traveladvisory
        {
        }

        public class Localizedvalues
        {
            public Distance distance { get; set; }
            public Duration duration { get; set; }
            public Staticduration staticDuration { get; set; }
        }

        public class Distance
        {
            public string text { get; set; }
        }

        public class Duration
        {
            public string text { get; set; }
        }

        public class Staticduration
        {
            public string text { get; set; }
        }

        public class Polylinedetails
        {
        }

        public class Leg
        {
            public int distanceMeters { get; set; }
            public string duration { get; set; }
            public string staticDuration { get; set; }
            public Polyline polyline { get; set; }
            public Startlocation startLocation { get; set; }
            public Endlocation endLocation { get; set; }
            public Step[] steps { get; set; }
            public Localizedvalues localizedValues { get; set; }
        }



        public class Startlocation
        {
            public Latlng latLng { get; set; }
        }


        public class Endlocation
        {
            public Latlng latLng { get; set; }
        }

        public class Step
        {
            public int distanceMeters { get; set; }
            public string staticDuration { get; set; }
            public Polyline polyline { get; set; }
            public Startlocation startLocation { get; set; }
            public Endlocation endLocation { get; set; }
            public Navigationinstruction navigationInstruction { get; set; }
            public Localizedvalues localizedValues { get; set; }
            public string travelMode { get; set; }
        }

        public class Navigationinstruction
        {
            public string maneuver { get; set; }
            public string instructions { get; set; }
        }

    }
}
