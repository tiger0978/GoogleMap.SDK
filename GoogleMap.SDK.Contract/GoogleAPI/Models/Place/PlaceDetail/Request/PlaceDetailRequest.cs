using GoogleMap.SDK.API.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.GoogleAPI.Models.PlaceDetail.Request
{
    public class PlaceDetailRequest : BaseRequest
    {
        public string baseUrl = "place/details/json?";
        public string place_id { get; set; }

        public PlaceDetailRequest(string place_id)
        {
            this.place_id = place_id;
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
