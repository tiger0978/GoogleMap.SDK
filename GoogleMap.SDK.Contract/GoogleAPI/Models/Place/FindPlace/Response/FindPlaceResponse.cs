using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.GoogleAPI.Models.FindPlace.Response
{
    public class FindPlaceResponse
    {
        public Candidate[] candidates { get; set; }
        public string status { get; set; }

        public class Candidate
        {
            public string formatted_address { get; set; }
            public Geometry geometry { get; set; }
            public string name { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
            public Viewport viewport { get; set; }
        }

        public class Location
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Viewport
        {
            public Northeast northeast { get; set; }
            public Southwest southwest { get; set; }
        }

        public class Northeast
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

        public class Southwest
        {
            public float lat { get; set; }
            public float lng { get; set; }
        }

    }
}
