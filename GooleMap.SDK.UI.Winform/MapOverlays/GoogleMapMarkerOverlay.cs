using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using GoogleMap.SDK.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarkerType = GoogleMap.SDK.Contract.Commons.Enums.GMarkerGoogleType;
using GoogleMarkerType = GMap.NET.WindowsForms.Markers.GMarkerGoogleType;



namespace GooleMap.SDK.UI.Winform
{
    public class GoogleMapMarkerOverlay : AMapOverlay
    {
        public GoogleMapMarkerOverlay(string overlayId) : base(overlayId)
        {
        }

        public override void SetOverLay(IEnumerable<List<Latlng>> lists, MarkerType markerType = MarkerType.red_dot, object toolTip = null)
        {
            throw new NotImplementedException("This Method is not Supported");
        }

        public override void SetOverLay(double lat, double lng, MarkerType markerType = MarkerType.red_dot, object toolTip = null)
        {
            GMapToolTip gMapToolTip = toolTip as GMapToolTip;   
            GMapMarker marker = AddMarker(lat, lng, markerType, gMapToolTip);
            this.Markers.Add(marker);
        }

        public override void SetOverLay(IEnumerable<Latlng> list, MarkerType markerType = MarkerType.red_dot, object toolTip = null)
        {
            GMapToolTip gMapToolTip = toolTip as GMapToolTip;
            this.Clear();
            int markerIndex = 0;
            foreach (var pointLatLng in list)
            {
                GMapMarker marker = AddMarker(pointLatLng.latitude, pointLatLng.longitude, markerType, gMapToolTip);
                this.Markers.Add(marker);
                marker.Tag = markerIndex++;
            }
        }

        public override void SetOverLay(IEnumerable<Location> locations, MarkerType markerType = MarkerType.red_dot, object toolTip = null)
        {
            GMapToolTip gMapToolTip = toolTip as GMapToolTip;
            this.Clear();
            int markerIndex = 0;
            foreach (var location in locations)
            {
                GMapMarker marker = AddMarker(location.latLng.latitude, location.latLng.longitude, markerType, gMapToolTip);

                this.Markers.Add(marker);
                marker.Tag = markerIndex++;
            }
        }

        private GMapMarker AddMarker(double Lat, double Lng, MarkerType googleType, GMapToolTip toolTip = null)
        {
            GoogleMarkerType markerType = Mapper.Map<MarkerType,GoogleMarkerType>(googleType);
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(Lat, Lng), markerType);
            if (toolTip != null)
            {
                marker.ToolTip = toolTip;
            }
            return marker;
        }

        public override void DeleteOverlayElement(object element)
        {
            if(element is List<Location> locations)
            {
                foreach(var location in locations)
                {
                    var marker = Markers.FirstOrDefault(x => x.Position == new PointLatLng(location.latLng.latitude, location.latLng.longitude));
                    if (marker != null)
                    {
                        Markers.Remove(marker);
                    }
                }
            }
        }
    }
}
