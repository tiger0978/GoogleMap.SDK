using GoogleMap.SDK.API.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.GoogleAPI.Models.PlacePhoto.Request
{
    public class PlacePhotoRequest : BaseRequest
    {
        public string baseUrl = "place/photo?";
        public string photo_reference { get; set; }
        public int maxheight { get; set; }
        public PlacePhotoRequest(string photo_reference, int maxheight)
        {
            this.photo_reference = photo_reference;
            this.maxheight = maxheight;
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
