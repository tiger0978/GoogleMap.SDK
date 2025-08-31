using GMap.NET.WindowsPresentation;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.UI.WPF.MapOverlays
{
    public abstract class AMapOverlay : GMapMarker, IOverlay
    {

        public List<GMapMarker> markers = new List<GMapMarker>();
        public List<GMapMarker> routes = new List<GMapMarker>();
        public string overLayId;
        //protected Dictionary<string,List<GMapMarker>> markerOverlay = new Dictionary<string,List<GMapMarker>>();
        //protected Dictionary<string, List<GMapMarker>> routerOverlay = new Dictionary<string, List<GMapMarker>>();
        public AMapOverlay(string overLayId) : base(default)
        {
            this.overLayId = overLayId;
        }
        public void ClearAll()
        {
            this.markers.Clear();
            this.routes.Clear();
        }
        public abstract void SetOverLay(IEnumerable<List<Latlng>> lists, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void SetOverLay(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void SetOverLay(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void SetOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        public abstract void DeleteOverlayElement(object element);
    }
}
