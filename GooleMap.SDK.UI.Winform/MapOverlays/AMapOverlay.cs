using GMap.NET;
using GMap.NET.WindowsForms;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooleMap.SDK.UI.Winform
{
    public abstract class AMapOverlay : GMapOverlay, IOverlay
    {
        public AMapOverlay(string overlayId)
        {
            this.Id = overlayId;    
        }

        public void ClearAll()
        {
            this.Routes.Clear();
            this.Markers.Clear();

            //if (this is GoogleMapMarkerOverlay)
            //{
            //    this.Markers.Clear();
            //}
            //else if(this is GoogleMapRouteOverlay)
            //{
            //    this.Routes.Clear();
            //}
        }

        public abstract void SetOverLay(IEnumerable<List<Latlng>> lists, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void SetOverLay(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void SetOverLay(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void SetOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void DeleteOverlayElement(object element);
    }
}
