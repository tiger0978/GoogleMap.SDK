using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMap.SDK.Contracts.Commons.Models;

namespace GooleMap.SDK.UI.Winform
{
    public interface IGMap
    {
        void CreateRoute(IEnumerable<Latlng> points, GMapToolTip toolTip = null);
        void CreateRoute(IEnumerable<List<Latlng>> routePoints, GMapToolTip toolTip = null);
        void CreateRoute(IEnumerable<Location> locations, GMapToolTip toolTip = null);
        void CreateMarker(double lat, double lng, GMapToolTip toolTip = null);
        void CreateMarker(IEnumerable<Latlng> list, GMapToolTip toolTip = null);
        void CreateMarker(IEnumerable<Location> locations, GMapToolTip toolTip = null);
        void ClearMarkers();
        void ClearRoutes();
        void ClearAll();
        void DeleteMarker(Location location);
   
    }
}
