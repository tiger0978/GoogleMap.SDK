using GoogleMap.SDK.API.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.API.Services.Place.NearBySearch.Request
{
    public class NearBySearchRequest : BaseRequest
    {
        public string baseUrl = "place/nearbysearch/json?";
        public string location { get; set; }
        public string radius { get; set; }

        public string keyword;

        public string type;

        public string Keyword
        {
            get
            {
                return keyword;
            }
            set
            {
                keyword = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public NearBySearchRequest(string location, string radius)
        {
            this.location = location;
            this.radius = radius;
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
