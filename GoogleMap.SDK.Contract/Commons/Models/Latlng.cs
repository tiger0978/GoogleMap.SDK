using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contracts.Commons.Models
{
    public class Latlng
    {
        public Latlng() { }
        public Latlng(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double latitude { get; set; }
        public double longitude { get; set; }
    }
}
