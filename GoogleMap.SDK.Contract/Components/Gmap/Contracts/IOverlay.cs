using GoogleMap.SDK.Contract.Commons.Enums;
using GoogleMap.SDK.Contracts.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMap.SDK.Contract.Components.Gmap.Contracts
{
    public interface IOverlay
    {
        string Id { get; set; }
        void SetMarkerOverLay(IEnumerable<Location> locations, GMarkerGoogleType markerType = GMarkerGoogleType.red_dot, object toolTip = null);
        void SetRouteOverLay(IEnumerable<List<Latlng>> lists);
        void DeleteRouteElement(object element);
        void DeleteMarkerElement(object element);
        void ClearAll();
        void ClearMarkers();
        void ClearRoutes();


    }
}
