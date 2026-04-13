
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contract.Components.Gmap.Contracts;
using GoogleMap.SDK.Contract.Components.Gmap.Models;
using GoogleMap.SDK.Contracts.Commons.Models;

namespace GoogleMap.SDK.Contract
{
    public interface IGMap
    {
        event EventHandler<MarkerInfo> OnMarkerClicked;
        Location Position { get; set; }
        double Zoom { get; set; }

        List<string> GetOverLays();

        void CreateRoute(IEnumerable<Latlng> points, string overlayName = "MapOverlay");
        void CreateRoute(List<List<Latlng>> routePoints, string overlayName = "MapOverlay");
        void CreateRoute(IEnumerable<Location> locations, string overlayName = "MapOverlay");
        void CreateMarker(double lat, double lng, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null);
        void CreateMarker(IEnumerable<Latlng> list, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null);
        void CreateMarker(IEnumerable<Location> locations, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null);
        void CreateMarker(Location location, string overlayName = "MapOverlay", GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object data = null, object toolTip = null);

        void ClearOverlay(string overlayName = "MapOverlay");
        void ClearOverlay();
        void ClearRoutes(string overlayName = "MapOverlay");
        void ClearMarkers(string overlayName = "MapOverlay");
        void RemoveRouteElement(IEnumerable<List<Latlng>> routes, string overlayName = "MapOverlay");
        void RemoveMarkerElement(List<Location> locations, string overlayName = "MapOverlay");

        void ActivateRoute(int index, string overlayName = "MapOverlay");

        void HideOverlay(string overlayName = "MapOverlay");
        void ShowOverlay(string overlayName = "MapOverlay");
        void TryAddOverlayInGmapControlOverlays(IOverlay overlay);

    }
}
