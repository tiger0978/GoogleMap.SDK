
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;

namespace GooleMap.SDK.Contract
{
    public interface IGMap
    {

        Location Position { get; set; }
        double Zoom { get; set; }

        void CreateRoute(IEnumerable<Latlng> points);
        void CreateRoute(List<List<Latlng>> routePoints);
        void CreateRoute(IEnumerable<Location> locations);
        void CreateMarker(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);

        void CreateRoute(string overlayName, IEnumerable<Latlng> points);
        void CreateRoute(string overlayName, List<List<Latlng>> routePoints);
        void CreateRoute(string overlayName, IEnumerable<Location> locations);
        void CreateMarker(string overlayName, double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(string overlayName, IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(string overlayName, IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);

        void ClearOverlay(string overlayName = "MapOverlay");
        void ClearRoutes(string overlayName = "MapOverlay");
        void ClearMarkers(string overlayName = "MapOverlay");
        void RemoveRouteElement(IEnumerable<List<Latlng>> routes, string overlayName = "MapOverlay");
        void RemoveMarkerElement(List<Location> locations, string overlayName = "MapOverlay");

        void HideOverlay(string overlayName = "MapOverlay");
        void ShowOverlay(string overlayName = "MapOverlay");
    }
}
