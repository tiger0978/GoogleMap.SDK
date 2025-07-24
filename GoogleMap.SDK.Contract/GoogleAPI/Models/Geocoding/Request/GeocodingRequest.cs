using GoogleMap.SDK.API.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.GoogleAPI.Models.Geocoding.Request
{
    public class GeocodingRequest : BaseRequest
    {
        //public string place_id {  get; set; }
        public string address { get; set; }
        public string baseUrl = "geocode/json?";
        public string bounds;

        public string Bounds
        {
            get
            {
                return bounds;
            }
            set
            {
                bounds = value;
            }
        }
        public GeocodingRequest(string address)
        {
            this.address = address;
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
