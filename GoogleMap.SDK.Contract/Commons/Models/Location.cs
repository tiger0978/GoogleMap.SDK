using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.Commons.Models
{
    public class Location
    {
        public Location() { }
        public Location(double lat, double lng)
        {
            latLng = new Latlng(lat, lng);
        }
        public Latlng latLng { get; set; } 
    }
}
