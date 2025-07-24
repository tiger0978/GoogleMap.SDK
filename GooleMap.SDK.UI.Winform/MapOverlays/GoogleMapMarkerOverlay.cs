using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooleMap.SDK.UI.Winform
{
    public class GoogleMapMarkerOverlay : AMapOverlay
    {
        public override void SetOverLay(IEnumerable<List<Latlng>> lists, GMapToolTip toolTip = null)
        {
            throw new NotImplementedException("This Method is not Supported");
        }

        public override void SetOverLay(double lat, double lng, GMapToolTip toolTip = null)
        {
            GMapMarker marker = AddMarker(lat, lng, toolTip);
            this.Markers.Add(marker);
        }

        public override void SetOverLay(IEnumerable<Latlng> list, GMapToolTip toolTip = null)
        {
            this.Clear();
            int markerIndex = 0;
            foreach (var pointLatLng in list)
            {
                GMapMarker marker = AddMarker(pointLatLng.latitude, pointLatLng.longitude, toolTip);
                this.Markers.Add(marker);
                marker.Tag = markerIndex++;
            }
        }

        public override void SetOverLay(IEnumerable<Location> locations, GMapToolTip toolTip = null)
        {
            this.Clear();
            int markerIndex = 0;
            foreach (var location in locations)
            {
                GMapMarker marker = AddMarker(location.latLng.latitude, location.latLng.longitude, toolTip);

                this.Markers.Add(marker);
                marker.Tag = markerIndex++;
            }
        }

        private GMapMarker AddMarker(double Lat, double Lng, GMapToolTip toolTip = null)
        {
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(Lat, Lng), GMarkerGoogleType.red_small);
            if (toolTip != null)
            {
                marker.ToolTip = toolTip;
            }
            return marker;
        }
    }
}
