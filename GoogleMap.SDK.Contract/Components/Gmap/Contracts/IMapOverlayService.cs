using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.Components.Gmap.Contracts
{
    public interface IMapOverlayService
    {
        IOverlayNew CreateOverlay(string overlayId = "MapOverlay");
        IOverlayNew AddMarkers(IEnumerable<Location> locations, string overlayId = "MapOverlay",  GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        IOverlayNew AddRoutes(IEnumerable<List<Latlng>> routes, string overlayId = "MapOverlay");
        void DeleteMarkers(string overlayId = "MapOverlay");
        void DeleteRoutes(string overlayId = "MapOverlay");
        IOverlayNew DeleteOverlay(string overlayId = "MapOverlay");
        void DeleteMarkerElement(object element, string overlayId = "MapOverlay");
        void DeleteRouteElement(object element, string overlayId = "MapOverlay");
        IOverlayNew GetOverlay(string overlayId = "MapOverlay");
    }
}
