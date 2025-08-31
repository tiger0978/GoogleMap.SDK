
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

        void CreateRoute(IEnumerable<Latlng> points, object toolTip = null);
        void CreateRoute(IEnumerable<List<Latlng>> routePoints, object toolTip = null);
        void CreateRoute(IEnumerable<Location> locations, object toolTip = null);
        void CreateMarker(double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);

        void CreateRoute(string overlayName, IEnumerable<Latlng> points, object toolTip = null);
        void CreateRoute(string overlayName, IEnumerable<List<Latlng>> routePoints, object toolTip = null);
        void CreateRoute(string overlayName, IEnumerable<Location> locations, object toolTip = null);
        void CreateMarker(string overlayName, double lat, double lng, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(string overlayName, IEnumerable<Latlng> list, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void CreateMarker(string overlayName, IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);

        void ClearMarkerOverlay();
        void ClearRouteOverlay();
        void ClearMarkerOverlay(string overlayName);
        void ClearRouteOverlay(string overlayName);
        void ClearAll();
        void ClearAll(string overlayName);
        void DeleteMarkers(List<Location> locations);
        void DeleteMarkers(string overlayName, List<Location> locations);
        void DeleteRoutes(IEnumerable<List<Latlng>> routes);
        void DeleteRoutes(string overlayName, IEnumerable<List<Latlng>> routes);

    }
}
