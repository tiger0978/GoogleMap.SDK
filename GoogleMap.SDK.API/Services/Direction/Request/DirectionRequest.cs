using GoogleMap.SDK.API.Commons;
using GoogleMap.SDK.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Models.Requests
{
    public class DirectionRequest : BaseRequest
    {
        public string baseUrl = "directions/json?";
        private string destination { get; set; }
        private string origin { get; set; }
        public string avoid { get; set; }
        private string mode { get; set; }

        public string transit_mode;

        public string waypoints;
        public string alternatives { get; set; } = "true";

        public DirectionRequest(string origin, string destination, IEnumerable<Avoid> avoids, TrafficMode mode)
        {
            foreach (Avoid avoid in avoids)
            {
                this.avoid += avoid.ToString() + "|";
            }
            this.avoid = this.avoid.TrimEnd('|');
            this.mode = mode.ToString();
            this.origin = origin;
            this.destination = destination;
        }

        public string Transit_mode
        {
            get { return this.transit_mode; }
            set { this.transit_mode = value; }
        }

        public string Waypoints
        {
            get { return this.waypoints; }
            set
            {
                //this.waypoints = this.mode == Mode.transit.ToString() ? "" : value;
                this.waypoints = value;
            }
        }

        public override string URL
        {
            get
            {
                string url = baseUrl + base.ToUri();
                return url;
            }
        }
    }
}
