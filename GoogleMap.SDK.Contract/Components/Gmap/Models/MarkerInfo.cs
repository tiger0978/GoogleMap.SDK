using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.Components.Gmap.Models
{
    public class MarkerInfo
    {
        public object Tag { get; set; }
        public Location Location { get; set; }

        public object Marker { get; set; }

        public MarkerInfo(Location location, object marker, object tag)
        {
            this.Location = location;
            this.Marker = marker;
            this.Tag = tag;
        }

    }
}
